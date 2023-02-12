using System.Collections.Generic;
using System.Windows.Input;
using WisdomLight.View;
using WisdomLight.ViewModel.Components.Building.Components.Filler.Blocks;
using WisdomLight.ViewModel.Components.Core.Commands;
using WisdomLight.ViewModel.Components.Data.Units.Fields;
using WisdomLight.ViewModel.Components.Data.Units.Fields.Tools;

namespace WisdomLight.ViewModel.Components.Building.Components.Filler.Tabs
{
    public abstract class InformationFiller : FillerFile
    {
        public ICommand _addInformation;
        public ICommand _dropInformation;

        public InformationFiller(IWindowService window) : base(window) { }

        public override FillerBuilder2 Add()
        {
            _addInformation = new RelayCommand(argument =>
            {
                TextExpression current = new TextExpression() { Type = "Текст" };
                List<IExpression> expressions = new List<IExpression>
                {
                    current,
                    new NumberExpression() { Type = "Число" },
                    new DateExpression() { Type = "Дата" }
                };

                FieldSelector additor = new FieldSelector(expressions)
                {
                    Selected = 0
                };
                _viewModel.Data.Information.Add(additor);
            });
            return this;
        }

        public override FillerBuilder2 Drop()
        {
            _dropInformation = new RelayCommand(argument => _viewModel.Data.Information.RemoveSelected());
            return this;
        }

        private protected override FillerBuilder2 ViewModel()
        {
            _ = base.ViewModel();
            return Add().Drop();
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
            return this;
        }

        public override FileViewModel Build()
        {
            FileViewModel viewModel = base.Build();
            viewModel.AddInformation = _addInformation;
            viewModel.DropInformation = _dropInformation;
            return viewModel;
        }
    }
}
