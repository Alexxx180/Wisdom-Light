using System.Windows.Forms;

namespace WisdomLight.Model
{
    public struct Confirmer
    {
        public Confirmer(DialogResult result, string path)
        {
            Result = result;
            Path = path;
        }

        public string Path { get; }
        public DialogResult Result { get; }
    }
}
