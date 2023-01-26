using WisdomLight.Model.Exceptions.IO;
using WisdomLight.ViewModel.Components.Core.Dialogs;
using WisdomLight.ViewModel.Components.Data;
using static WisdomLight.ViewModel.Components.Building.Bank.Serialization;

namespace WisdomLight.ViewModel.Components.Core.Processors.Serialization.Objects
{
    public class FileFiller
    {
        public FileFiller()
        {
            _processors = Serializers();
        }

        internal void FixedSave(string path, TemplateViewModel program)
        {
            Save(_processor.FixExtension(path), program);
        }

        internal void Save(string path, TemplateViewModel program)
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

        internal TemplateViewModel Load(string path)
        {
            TemplateViewModel template = null;

            try
            {
                template = _processor.Read<TemplateViewModel>(path);
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
