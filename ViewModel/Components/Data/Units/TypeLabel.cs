namespace WisdomLight.ViewModel.Data
{
    public class TypeLabel : NameLabel
    {
        private string _type;
        public virtual string Type
        {
            get => _type;
            set
            {
                _type = value;
                OnPropertyChanged();
            }
        }
    }
}
