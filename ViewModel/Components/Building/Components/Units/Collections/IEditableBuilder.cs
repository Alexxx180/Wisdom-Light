using WisdomLight.Model;
using WisdomLight.ViewModel.Components.Data.Units.Collections;

namespace WisdomLight.ViewModel.Components.Building.Collections
{
    public interface IEditableBuilder<T> where T : ICloneable<T>
    {
        public IEditableBuilder<T> Fields();

        public IEditableBuilder<T> SelectedItems();

        public IEditableBuilder<T> Reset();

        public EditableCollection<T> Build();
    }
}
