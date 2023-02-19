using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Windows.Input;
using WisdomLight.Model;
using WisdomLight.Model.Results.Confirming;
using WisdomLight.View;
using WisdomLight.ViewModel.Components.Building.Bank;
using WisdomLight.ViewModel.Components.Building.Filler;
using WisdomLight.ViewModel.Components.Building.Main.Preferences;
using WisdomLight.ViewModel.Components.Core.Commands;
using WisdomLight.ViewModel.Components.Core.Dialogs;
using WisdomLight.ViewModel.Components.Core.Dialogs.Traditional.Manager;
using WisdomLight.ViewModel.Components.Core.Processors;
using WisdomLight.ViewModel.Components.Core.Processors.Serialization;
using WisdomLight.ViewModel.Components.Core.Processors.Serialization.Objects;
using WisdomLight.ViewModel.Components.Data;
using WisdomLight.ViewModel.Components.Data.Units;

namespace WisdomLight.ViewModel.Components.Building.Main
{
    public class MainBuilder : IMainBuilder
    {
        private const string Settings = "config.json";

#warning Potential solution for re-focus after text input...
        /// <summary>
        /// if (Keyboard.FocusedElement is TextBox textBox)
        /// {
        ///     TraversalRequest tRequest = new
        ///     TraversalRequest(FocusNavigationDirection.Next);
        ///     _ = textBox.MoveFocus(tRequest);
        /// }
        /// </summary>

        private IWindowService _windows;
        private IDialogService<NameViewModel> _naming;

        private MainViewModel _viewModel;
        private PreferencesViewModel _data;
        private PreferencesFiller _serializer;

        private IPreferencesBuilder _preferencesBuilder;
        private IFillerBuilder _filler;

        public ICommand _addInformation;
        public ICommand _dropInformation;
        public ICommand _openTemplate;

        public ICommand _addLink;
        public ICommand _dropLink;
        public ICommand _openDependency;
        public ICommand _renameDependency;
        
        public ICommand _newCommand;
        public ICommand _openCommand;
        public ICommand _openFromCommand;

        public ICommand _closeCommand;

        public ICommand _importCommand;
        public ICommand _exportCommand;

        public ICommand _saveCommand;

        public ICommand _searchCommand;

        private bool _canClose;

        public MainBuilder(IWindowService windows, IDialogService<DependenciesViewModel> dialog)
        {
            _windows = windows;
            _filler = new FillerBuilder(_windows, dialog);
            _preferencesBuilder = new PreferencesBuilder();
            _serializer = new PreferencesFiller
            {
                Current = 0
            };
            _naming = new NameDialog();
        }

        private void ExportNode(ZipArchive zip, DependenciesNode node, Stack<string> path)
        {
            if (node.IsDependency)
            {
                string relative = Path.Combine(path.ToArray());
                string extension = Path.GetExtension(node.DependencyPath);
                string dependency = Path.Combine(relative, Path.ChangeExtension(node.Name, extension));

                zip.CreateEntryFromFile(node.DependencyPath, dependency);
                node.DependencyPath = dependency;
            }

            if (node.Nodes.Count > 0)
            {
                path.Push(node.Name);
                ExportNodes(zip, node, path);
                path.Pop();
            }
        }

        private void ExportNodes(ZipArchive zip, DependenciesNode parent, Stack<string> path)
        {
            for (int i = 0; i < parent.Nodes.Count; i++)
            {
                ExportNode(zip, parent.Nodes[i], path);
            }
        }

        private void ExportNodes(ZipArchive zip, DependenciesViewModel parent, Stack<string> path)
        {
            for (int i = 0; i < parent.Dependencies.Count; i++)
            {
                ExportNode(zip, parent.Dependencies[i], path);
            }
        }

        private void CreateManifest(ZipArchive zip, PreferencesViewModel preferences)
        {
            string temporary = Path.ChangeExtension(Path.GetTempFileName(), JsonProcessor.Extension);
            _viewModel.Serializer.Save(temporary, preferences);
            zip.CreateEntryFromFile(temporary, Settings);
            FileProcessor.Delete(temporary);
        }

        public IMainBuilder Export()
        {
            _exportCommand = new RelayCommand(
                argument =>
                {
                    KeyConfirmer dialog = ProjectManager.Export(_viewModel.Data.SelectedLocation, string.Empty);
                    if (!dialog.Result)
                        return;

                    string dependencies = Path.Combine(Defaults.Runtime, Settings);
                    using (ZipArchive zip = ZipFile.Open(dialog.Path, ZipArchiveMode.Create))
                    {
                        Stack<string> path = new Stack<string>();
                        PreferencesViewModel preferences = _viewModel.Data.Clone();

                        ExportNodes(zip, preferences.DependencyTree, path);
                        ExportNodes(zip, preferences.GenerationTree, path);

                        CreateManifest(zip, preferences);
                    }
                }
            );
            return this;
        }

        private void RebuildDependency(DependenciesNode node)
        {
            if (node.IsDependency)
            {
                node.DependencyPath = Path.Combine(Defaults.Runtime, node.DependencyPath);
            }

            if (node.Nodes.Count > 0)
            {
                RebuildDependencies(node);
            }
        }

        private void RebuildDependencies(DependenciesNode parent)
        {
            for (int i = 0; i < parent.Nodes.Count; i++)
            {
                RebuildDependency(parent.Nodes[i]);
            }
        }

        private void RebuildDependencies(DependenciesViewModel parent)
        {
            for (int i = 0; i < parent.Dependencies.Count; i++)
            {
                RebuildDependency(parent.Dependencies[i]);
            }
            parent.Relate();
        }

        public IMainBuilder Import()
        {
            _importCommand = new RelayCommand(
                argument =>
                {
                    ReConfirmer dialog = ProjectManager.Open(_viewModel.Data.SelectedLocation);
                    if (!dialog.Result)
                        return;

                    using (ZipArchive zip = ZipFile.Open(dialog.FullPath, ZipArchiveMode.Read))
                    {
                        foreach (ZipArchiveEntry entry in zip.Entries)
                        {
                            if (entry.FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase) ||
                                entry.FullName.EndsWith(".docx", StringComparison.OrdinalIgnoreCase))
                            {
                                // Gets the full path to ensure that relative segments are removed.
                                string destination = Path.GetFullPath(Path.Combine(Defaults.Runtime, entry.FullName));

                                // Ordinal match is safest, case-sensitive volumes can be mounted within volumes that
                                // are case-insensitive.
                                if (destination.StartsWith(Defaults.Runtime, StringComparison.Ordinal))
                                {
                                    Directory.CreateDirectory(Path.GetDirectoryName(destination));
                                    entry.ExtractToFile(destination, true);
                                }
                            }
                        }
                    }

                    string dependencies = Path.Combine(Defaults.Runtime, Settings);
                    PreferencesViewModel preferences = _viewModel.Serializer.Load(dependencies);
                    RebuildDependencies(preferences.GenerationTree);
                    RebuildDependencies(preferences.DependencyTree);

                    _viewModel.Serializer.Save(dependencies, preferences);
                    _viewModel.Data = preferences;

                    _filler.SetDependencies(preferences.DependencyTree);
                }
            );
            return this;
        }

        private IFillerBuilder BaseFiller()
        {
            return _filler.Reset().NewFile().Open().CanClose().Close().Save().SaveAs().Export().Add().Drop().OpenLink();
        }

        private void SetDependencies(PreferencesViewModel viewModel)
        {
            _data = viewModel;
            _filler.SetDependencies(_data.DependencyTree);
        }

        private void SetDependencies()
        {
            SetDependencies(_preferencesBuilder.Serializer().Templates().Documents().Defend().DefaultPath().Build());
        }

        public IMainBuilder Save()
        {
            _saveCommand = new RelayCommand(
                argument =>
                {
                    string path = Path.Combine(Defaults.Runtime, Settings);
                    _viewModel.Serializer.Save(path, _viewModel.Data);
                }
            );
            return this;
        }

        public IMainBuilder Preferences()
        {
            string path = Path.Combine(Defaults.Runtime, Settings);
            if (!File.Exists(path))
            {
                SetDependencies();
                return this;
            }

            PreferencesViewModel viewModel = _serializer.Load(path);
            if (viewModel != null)
            {
                viewModel.DependencyTree.Relate();
                viewModel.GenerationTree.Relate();
                SetDependencies(viewModel);
                return this;
            }

            SetDependencies();
            return this;
        }

        public IMainBuilder AddLink()
        {
            _addLink = new RelayCommand(
                argument =>
                {
                    DependenciesNode current = _viewModel.Data.DependencyTree.SelectedDependency;
                    NameViewModel viewModel = new NameViewModel(current);
                    _naming.ShowDialog(viewModel, (result, selection) =>
                    {
                        if (result)
                            current.Add(selection.Name);
                    });
                },
                can =>
                {
                    DependenciesViewModel tree = _viewModel.Data.DependencyTree;
                    return (tree != null) && (tree.SelectedDependency != null);
                }
            );
            return this;
        }

        public IMainBuilder DropLink()
        {
            _dropLink = new RelayCommand(
                argument => _viewModel.Data.DependencyTree.SelectedDependency.Drop(),
                can =>
                {
                    DependenciesViewModel tree = _viewModel.Data.DependencyTree;
                    return (tree != null) && (tree.SelectedDependency != null) && (tree.SelectedDependency.Parent != null);
                }
            );
            return this;
        }

        public IMainBuilder OpenTemplate()
        {
            _openTemplate = new RelayCommand(
                argument =>
                {
                    ReConfirmer dialog = TemplateManager.Open(_viewModel.Data.SelectedLocation);
                    if (!dialog.Result)
                        return;

                    _viewModel.Data.GenerationTree.SelectedDependency.DependencyPath = dialog.FullPath;
                },
                canExecute => (_viewModel.Data.GenerationTree != null) && _viewModel.Data.GenerationTree.IsDependencySelected
            );
            return this;
        }

        public IMainBuilder OpenDependency()
        {
            _openDependency = new RelayCommand(
                argument =>
                {
                    ReConfirmer dialog = DocumentManager.Open(_viewModel.Data.SelectedLocation);
                    if (!dialog.Result)
                        return;

                    _viewModel.Data.DependencyTree.SelectedDependency.DependencyPath = dialog.FullPath;
                },
                canExecute => (_viewModel.Data.DependencyTree != null) && _viewModel.Data.DependencyTree.IsDependencySelected
            );
            return this;
        }

        public IMainBuilder RenameDependency()
        {
            _renameDependency = new RelayCommand(
                argument =>
                {
                    _viewModel.Data.DependencyTree.SelectedDependency.Name = argument.ToString();
                }
            );
            return this;
        }

        public IMainBuilder AddInformation()
        {
            _addInformation = new RelayCommand(
                argument =>
                {
                    DependenciesNode current = _viewModel.Data.GenerationTree.SelectedDependency;
                    NameViewModel viewModel = new NameViewModel(current);
                    _naming.ShowDialog(viewModel, (result, selection) =>
                    {
                        if (result)
                            current.Add(selection.Name);
                    });
                },
                can =>
                {
                    DependenciesViewModel tree = _viewModel.Data.GenerationTree;
                    return (tree != null) && (tree.SelectedDependency != null);
                }
            );
            return this;
        }

        public IMainBuilder DropInformation()
        {
            _dropInformation = new RelayCommand(
                argument => _viewModel.Data.GenerationTree.SelectedDependency.Drop(),
                can =>
                {
                    DependenciesViewModel tree = _viewModel.Data.GenerationTree;
                    return (tree != null) && (tree.SelectedDependency != null) && (tree.SelectedDependency.Parent != null);
                }
            );
            return this;
        }

        public IMainBuilder NewFile()
        {
            _newCommand = new RelayCommand(
                argument =>
                {
                    FileViewModel viewModel = BaseFiller().Template().OpenQuery().Build();

                    viewModel.Data.Name = "Новый документ";
                    viewModel.Data.Location = _viewModel.Data.SelectedLocation;

                    _windows.ShowWindow(viewModel);
                }
            );
            return this;
        }

        private void OpenFile(PreferencesViewModel preferences, ReConfirmer dialog)
        {
            if (!dialog.Result)
                return;

            preferences.Serializer.Current = dialog.Key;

            FileViewModel viewModel = BaseFiller().OpenQuery().Build();

            viewModel.Data = preferences.Serializer.Load(dialog.FullPath);
            viewModel.Data.Queriers.ViewModel = preferences.DependencyTree;
            viewModel.Data.Location = dialog.Path;
            viewModel.Data.FileName = dialog.Name;

            _windows.ShowWindow(viewModel);
        }

        public IMainBuilder Open()
        {
            _openCommand = new RelayCommand(
                argument =>
                {
                    ReConfirmer dialog = TemplateManager.Open(_viewModel.Data.SelectedLocation, _viewModel.Data.Serializer.Current);
                    OpenFile(_viewModel.Data, dialog);
                }
            );
            return this;
        }

        public IMainBuilder OpenFromTemplate()
        {
            _openFromCommand = new RelayCommand(
                argument =>
                {
                    DependenciesNode current = _viewModel.Data.GenerationTree.SelectedDependency;
                    string directory = Path.GetDirectoryName(current.DependencyPath);
                    string name = Path.GetFileName(current.DependencyPath);
                    ReConfirmer open = new ReConfirmer(0, name, directory, true);

                    OpenFile(_viewModel.Data, open);
                },
                can =>
                {
                    DependenciesViewModel tree = _viewModel.Data.GenerationTree;
                    return (tree != null) && (tree.SelectedDependency != null) && tree.SelectedDependency.IsDependency;
                }
            );
            return this;
        }

        public IMainBuilder Close()
        {
            _closeCommand = new RelayCommand(argument => _viewModel.Close?.Invoke(), can => _viewModel.CanClose);
            return this;
        }

        public IMainBuilder CanClose()
        {
            _canClose = true;
            return this;
        }
        
        public IMainBuilder Reset()
        {
            _canClose = false;
            _viewModel = null;
            _addInformation = null;
            _dropInformation = null;
            _openTemplate = null;
            _addLink = null;
            _dropLink = null;
            _openDependency = null;
            _renameDependency = null;
            _newCommand = null;
            _openCommand = null;
            _openFromCommand = null;
            _importCommand = null;
            _exportCommand = null;
            _searchCommand = null;
            _closeCommand = null;
            _preferencesBuilder.Reset();
            _filler.Reset();
            return this;
        }

        public MainViewModel Build()
        {
            _viewModel = new MainViewModel
            {
                Data = _data,
                AddInformation = _addInformation,
                DropInformation = _dropInformation,
                AddLink = _addLink,
                DropLink = _dropLink,
                OpenDependency = _openDependency,
                OpenTemplate = _openTemplate,
                OpenFromTemplate = _openFromCommand,
                RenameDependency = _renameDependency,
                NewCommand = _newCommand,
                OpenCommand = _openCommand,
                ImportCommand = _importCommand,
                ExportCommand = _exportCommand,
                SearchCommand = _searchCommand,
                SaveCommand = _saveCommand,
                CloseCommand = _closeCommand,
                CanClose = _canClose,
                Serializer = _serializer
            };
            return _viewModel;
        }
    }
}
