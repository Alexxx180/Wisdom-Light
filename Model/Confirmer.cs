using System.Windows.Forms;

namespace WisdomLight.Model
{
    public struct Confirmer
    {
        public Confirmer(string path, DialogResult result)
        {
            Path = path;
            Result = result;
        }

        public string Path { get; }
        public DialogResult Result { get; }
    }
}
