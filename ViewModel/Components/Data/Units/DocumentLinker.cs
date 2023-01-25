using WisdomLight.Model;
using WisdomLight.Model.Results.Confirming;

namespace WisdomLight.ViewModel.Components.Data.Units
{
    public class DocumentLinker : TypeLabel, ICloneable<DocumentLinker>
    {
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
