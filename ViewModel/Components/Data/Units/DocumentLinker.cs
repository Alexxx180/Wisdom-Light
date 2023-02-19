using WisdomLight.Model;
using WisdomLight.Model.Results.Confirming;

namespace WisdomLight.ViewModel.Components.Data.Units
{
    public class DocumentLinker : TypeLabel, ICloneable<DocumentLinker>
    {
        public DocumentLinker() { }

        public DocumentLinker(string same)
        {
            Name = same;
            Type = same;
        }

        private string _type;
        public override string Type
        {
            get => _type;
            set
            {
                _type = value;
                OnPropertyChanged();
            }
        }

        public void Set(ReConfirmer confirmer)
        {
            Name = confirmer.Name;
            Type = confirmer.Path;
        }

        public DocumentLinker Clone()
        {
            return new DocumentLinker
            {
                Name = Name,
                Type = Type
            };
        }
    }
}
