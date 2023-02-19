using System.Collections.Generic;
using WisdomLight.Model;

namespace WisdomLight.ViewModel.Components.Data.Units
{
    public class Querier : NotifyPropertyChanged, ICloneable<Querier>
    {
        protected internal const string PlaceHolder = "Выберите зависимость";
        public Stack<int> Path { get; set; }

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

        public void Push(int item)
        {
            Path.Push(item);
        }

        public int Pop()
        {
            return Path.Pop();
        }

        public void Clear()
        {
            Name = PlaceHolder;
            Path.Clear();
        }

        public Querier Clone()
        {
            return new Querier
            {
                Name = Name,
                Path = new Stack<int>(Path)
            };
        }
    }
}
