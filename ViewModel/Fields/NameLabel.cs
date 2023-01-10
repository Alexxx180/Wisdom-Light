namespace WisdomLight.ViewModel.Fields
{
    public abstract class NameLabel : NotifyPropertyChanged
    {
        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }
    }
}
