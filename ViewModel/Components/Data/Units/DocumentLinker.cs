using WisdomLight.Model;

namespace WisdomLight.ViewModel.Data
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
