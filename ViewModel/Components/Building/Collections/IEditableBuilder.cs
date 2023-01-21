using WisdomLight.Model;
using WisdomLight.ViewModel.Data.Collections;

namespace WisdomLight.ViewModel.Data.Files.Fields.Tools.Building.Filler.Collections
{
    public interface IEditableBuilder<T> where T : ICloneable<T>
    {
        public IEditableBuilder<T> Fields();

        public IEditableBuilder<T> SelectedItems();

        public IEditableBuilder<T> Reset();

        public EditableCollection<T> Build();
    }
}
