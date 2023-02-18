using WisdomLight.Model.Exceptions.IO;
using WisdomLight.ViewModel.Components.Core.Dialogs;
using WisdomLight.ViewModel.Components.Data;
using static WisdomLight.ViewModel.Components.Building.Bank.Serialization;

namespace WisdomLight.ViewModel.Components.Core.Processors.Serialization.Objects
{
    public class PreferencesFiller
    {
        public PreferencesFiller()
        {
            _processors = Serializers();
        }

        internal void FixedSave(string path, PreferencesViewModel program)
        {
            Save(_processor.FixExtension(path), program);
        }

        internal void Save(string path, PreferencesViewModel program)
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

        internal PreferencesViewModel Load(string path)
        {
            PreferencesViewModel template = null;

            try
            {
                template = _processor.Read<PreferencesViewModel>(path);
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
