using System.Windows.Forms;

namespace WisdomLight.Model
{
    public struct KeyConfirmer
    {
        public KeyConfirmer(byte key,
            string path, DialogResult result)
        {
            Key = key;
            Status = new Confirmer(path, result);
        }

        public byte Key { get; set; }
        public Confirmer Status { get; set; }
    }
}
