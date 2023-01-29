using WisdomLight.Model;
using WisdomLight.Model.Exceptions.IO;
using WisdomLight.ViewModel.Components.Core.Dialogs;
using WisdomLight.ViewModel.Components.Data;
using static WisdomLight.ViewModel.Components.Building.Bank.Serialization;

namespace WisdomLight.ViewModel.Components.Core.Processors.Serialization.Objects
{
    public class DependenciesFiller
    {
        public DependenciesFiller()
        {
            _processors = Serializers();
        }

        internal void FixedSave(string path, DependencyCounter program)
        {
            Save(_processor.FixExtension(path), program);
        }

        internal void Save(string path, DependencyCounter program)
        {
            try
            {
                _processor.Write(path, program);
            }
            catch (SaveException exception)
            {
                DialogManager.Message(exception);
            }
        }

        internal DependencyCounter Load(string path)
        {
            DependencyCounter template = null;

            try
            {
                template = _processor.Read<DependencyCounter>(path);
            }
            catch (ReadException exception)
            {
                DialogManager.Message(exception);
            }

            return template;
        }

        private byte _current;
        public byte Current
        {
            get => _current;
            set
            {
                _current = value;
                _processor = _processors[value];
            }
        }

        private FileProcessor[] _processors;
        private FileProcessor _processor;
    }
}
