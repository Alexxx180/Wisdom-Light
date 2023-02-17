namespace WisdomLight.ViewModel.Components.Data.Units
{
    public abstract class NameLabel : NotifyPropertyChanged
    {
        private string _name;
        public virtual string Name
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
