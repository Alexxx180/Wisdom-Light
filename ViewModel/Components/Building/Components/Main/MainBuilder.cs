using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Windows.Input;
using WisdomLight.Model.Results.Confirming;
using WisdomLight.View;
using WisdomLight.ViewModel.Components.Building.Bank;
using WisdomLight.ViewModel.Components.Building.Components.Filler;
using WisdomLight.ViewModel.Components.Building.Components.Filler.Tabs;
using WisdomLight.ViewModel.Components.Building.Extensions.Paths.Files;
using WisdomLight.ViewModel.Components.Building.Filler;
using WisdomLight.ViewModel.Components.Building.Main.Preferences;
using WisdomLight.ViewModel.Components.Core.Commands;
using WisdomLight.ViewModel.Components.Core.Dialogs;
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

        public ICommand _addLink;
        public ICommand _dropLink;

        public ICommand _renameDependency;
        public ICommand _openDependency;

        public ICommand _newCommand;
        public ICommand _openCommand;
        
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

        private void ExportNodes(ZipArchive zip, DependenciesNode parent, Stack<string> path)
        {
            for (int i = 0; i < parent.Nodes.Count; i++)
            {
                DependenciesNode node = parent.Nodes[i];
                
                if (node.IsDependency)
                {
                    string relative = Path.Combine(path.ToArray());
                    string dependency = Path.Combine(relative, Path.GetFileName(node.DependencyPath));

                    string absolute = Path.Combine(_viewModel.Data.SelectedLocation, relative);
                    zip.CreateEntryFromFile(node.DependencyPath, dependency);
                }
                
                if (node.Nodes.Count > 0)
                {
                    path.Push(node.Name);
                    ExportNodes(zip, node, path);
                    path.Pop();
                }
            }
        }

        public IMainBuilder Export()
        {
            _exportCommand = new RelayCommand(
                argument =>
                {
                    string dependencies = Path.Combine(Defaults.Runtime, Settings);
                    string archive = Path.Combine(Defaults.Runtime, "test.zip");
                    Stack<string> path = new Stack<string>();

                    using (ZipArchive zip = ZipFile.Open(archive, ZipArchiveMode.Create))
                    {
                        for (int i = 0; i < _data.DependencyTree.Dependencies.Count; i++)
                        {
                            DependenciesNode node = _data.DependencyTree.Dependencies[i];
                            
                            if (node.IsDependency)
                            {
                                string relative = Path.Combine(path.ToArray());
                                string dependency = Path.Combine(relative, Path.GetFileName(node.DependencyPath));

                                string absolute = Path.Combine(_viewModel.Data.SelectedLocation, relative);
                                zip.CreateEntryFromFile(node.DependencyPath, dependency);
                            }

                            if (node.Nodes.Count > 0)
                            {
                                path.Push(node.Name);
                                ExportNodes(zip, node, path);
                                path.Pop();
                            }
                        }

                        zip.CreateEntryFromFile(dependencies, Settings);
                    }
                }
            );
            return this;
        }

        public IMainBuilder Import()
        {
            throw new NotImplementedException();
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
                    DependenciesNode current = _data.DependencyTree.SelectedDependency;
                    NameViewModel viewModel = new NameViewModel(current);
                    _naming.ShowDialog(viewModel, (result, selection) =>
                    {
                        if (result)
                            current.Add(selection.Name);
                    });
                }
            );
            return this;
        }

        public IMainBuilder DropLink()
        {
            _dropLink = new RelayCommand(
                argument => _data.DependencyTree.SelectedDependency.Drop(),
                can =>
                {
                    DependenciesViewModel tree = _data.DependencyTree;
                    return (tree != null) && (tree.SelectedDependency != null) && (tree.SelectedDependency.Parent != null);
                }
            );
            return this;
        }

        public IMainBuilder OpenDependency()
        {
            _openDependency = new RelayCommand(
                argument =>
                {
                    ReConfirmer dialog = DialogManager.Template(_viewModel.Data.SelectedLocation);
                    if (!dialog.Result)
                        return;

                    _data.DependencyTree.SelectedDependency.DependencyPath = dialog.FullPath;
                },
                canExecute => (_data.DependencyTree != null) && _data.DependencyTree.IsDependencySelected
            );
            return this;
        }

        public IMainBuilder RenameDependency()
        {
            _renameDependency = new RelayCommand(
                argument =>
                {
                    _data.DependencyTree.SelectedDependency.Name = argument.ToString();
                }
            );
            return this;
        }

        public IMainBuilder AddInformation()
        {
            _addInformation = new RelayCommand(
                argument =>
                {
                    DependenciesNode current = _data.GenerationTree.SelectedDependency;
                    NameViewModel viewModel = new NameViewModel(current);
                    _naming.ShowDialog(viewModel, (result, selection) =>
                    {
                        if (result)
                            current.Add(selection.Name);
                    });
                }
            );
            return this;
        }

        public IMainBuilder DropInformation()
        {
            _dropInformation = new RelayCommand(
                argument => _data.GenerationTree.SelectedDependency.Drop(),
                can =>
                {
                    DependenciesViewModel tree = _data.GenerationTree;
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

        public IMainBuilder Open()
        {
            _openCommand = new RelayCommand(
                argument =>
                {
                    ReConfirmer dialog = DialogManager.Open(_viewModel.Data.SelectedLocation, _viewModel.Data.Serializer.Current);
                    if (!dialog.Result)
                        return;

                    _viewModel.Data.Serializer.Current = dialog.Key;

                    FileViewModel viewModel = BaseFiller().OpenQuery().Build();
                    
                    viewModel.Data = _viewModel.Data.Serializer.Load(dialog.FullPath);
                    viewModel.Data.Location = dialog.Path;
                    viewModel.Data.FileName = dialog.Name;

                    _windows.ShowWindow(viewModel);
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
            _addLink = null;
            _dropLink = null;
            _openDependency = null;
            _renameDependency = null;
            _newCommand = null;
            _openCommand = null;
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
