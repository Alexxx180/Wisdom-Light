using System.Collections.Generic;
using System.Windows.Forms;
using WisdomLight.Model;
using static WisdomLight.Writers.AutoGenerating.Processors;

namespace WisdomLight.Writers
{
    public static class ResultRenderer
    {
        public static readonly string TemplateFilter;

        static ResultRenderer()
        {
            Files = new HashSet<string>();
            TemplateFilter = "Документ Microsoft Word (*.docx)|*.docx";
        }

        public static Pair<string, bool> UserAgreement()
        {
            using FolderBrowserDialog
                dialog = CallLocator("Выберите место для сохранения");

            bool isAgreed = dialog.ShowDialog() == DialogResult.OK;
            System.Diagnostics.Trace.WriteLine("Location set: " + isAgreed);

            Pair<string, bool>
                head = new Pair<string, bool>
                (dialog.SelectedPath, isAgreed);

            return head;
        }

        public static HashSet<string> Files { get; set; }
    }
}