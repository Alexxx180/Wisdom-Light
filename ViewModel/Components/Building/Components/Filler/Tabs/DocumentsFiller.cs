using System.Text;
using System.Windows.Input;
using WisdomLight.Model.Results.Confirming;
using WisdomLight.View;
using WisdomLight.ViewModel.Components.Building.Bank;
using WisdomLight.ViewModel.Components.Core.Commands;
using WisdomLight.ViewModel.Components.Core.Dialogs;
using WisdomLight.ViewModel.Components.Core.Dialogs.Traditional.Manager;
using WisdomLight.ViewModel.Components.Data;
using WisdomLight.ViewModel.Components.Data.Units;

namespace WisdomLight.ViewModel.Components.Building.Components.Filler.Tabs
{
    public class DocumentsFiller : InformationFiller
    {
        private readonly StringBuilder _path;
        private IDialogService<DependenciesViewModel> _dependenciesDialog;

        public ICommand _addLink;
        public ICommand _dropLink;
        public ICommand _openLink;

        public ICommand _openQuery;

        public DocumentsFiller(IWindowService window,
            IDialogService<DependenciesViewModel> dialog) : base(window)
        {
            _dependenciesDialog = dialog;
            _path = new StringBuilder();
        }

        public override FillerBuilder2 OpenLink()
        {
            _openLink = new RelayCommand(
                argument =>
                {
                    ReConfirmer confirmer = DocumentManager.Open(Defaults.Runtime);
                    if (!confirmer.Result)
                        return;

                    for (int i = 0; i < _viewModel.Data.Links.SelectedItems.Count; i++)
                    {
                        _viewModel.Data.Links.SelectedItems[i].Set(confirmer);
                    }
                }
            );
            return this;
        }

        public override FillerBuilder2 OpenQuery(DependenciesViewModel dependencies)
        {
            _openQuery = new RelayCommand(argument =>
                _dependenciesDialog.ShowDialog(dependencies, (result, selection) =>
                {
                    if (!result)
                        return;
                    if (!selection.SelectedDependency.IsDependency)
                        return;

                    for (int i = 0; i < _data.Queriers.SelectedItems.Count; i++)
                    {
                        _data.Queriers.SelectedItems[i].Clear();
                    }

                    DependenciesNode current = selection.SelectedDependency;
                    _path.Insert(0, current.Name);
                    do
                    {
                        for (int i = 0; i < _data.Queriers.SelectedItems.Count; i++)
                        {
                            _data.Queriers.SelectedItems[i].Push(current.No);
                        }
                        current = current.Parent;
                        _path.Insert(0, $"{current.Name}/");
                    }
                    while (current != null);

                    for (int i = 0; i < _data.Queriers.SelectedItems.Count; i++)
                    {
                        _data.Queriers.SelectedItems[i].Name = _path.ToString();
                    }
                    _path.Clear();
                })
            );
            return this;
        }

        public override FillerBuilder2 Add()
        {
            base.Add();
            _addLink = new RelayCommand(argument => _viewModel.Data.Links.Add(new DocumentLinker(string.Empty)));
            return this;
        }

        public override FillerBuilder2 Drop()
        {
            base.Drop();
            _dropInformation = new RelayCommand(argument => _viewModel.Data.Information.RemoveSelected());
            return this;
        }

        private protected override FillerBuilder2 ViewModel()
        {
            _ = base.ViewModel();
            return this;
        }

        public override FillerBuilder2 Template()
        {
            _ = base.Template();
            return this;
        }

        public override FillerBuilder2 Reset()
        {
            _ = base.Reset();
            _addInformation = null;
            _dropInformation = null;
            _openLink = null;
            _openQuery = null;
            return this;
        }

        public override FileViewModel Build()
        {
            FileViewModel viewModel = base.Build();
            viewModel.AddLink = _addLink;
            viewModel.DropLink = _dropLink;
            viewModel.OpenLink = _openLink;
            viewModel.OpenQuery = _openQuery;
            return viewModel;
        }
    }
}
