using System.IO;
using System.Collections.Generic;
using WisdomLight.Model.Exceptions.IO;
using WisdomLight.ViewModel.Components.Core.Dialogs;
using WisdomLight.ViewModel.Components.Data.Units;
using WisdomLight.ViewModel.Components.Data.Units.Fields;

namespace WisdomLight.ViewModel.Components.Core.Processors.Export.Documents
{
    internal abstract class FileDocument : Saver, IDocument
    {
        public void Export(IList<DocumentLinker> paths, List<IExpression> expressions, string folder)
        {
            for (byte i = 0; i < paths.Count; i++)
            {
                string template = paths[i].Type;

                if (!File.Exists(template))
                    continue;

                TemplateFrom(template).GenerateTo(folder + Path.GetFileName(template));

                try
                {
                    Process(expressions);
                }
                catch (SaveException exception)
                {
                    DialogManager.Message(exception);
                }
            }
        }

        /// <summary>
        /// Set full template path to process
        /// </summary>
        /// <param name="template">Path to original template</param>
        internal abstract FileDocument TemplateFrom(string template);

        /// <summary>
        /// Set new document path to generate to
        /// </summary>
        /// <param name="document">Saved document full path</param>
        internal abstract FileDocument GenerateTo(string document);

        /// <summary>
        /// Process and save the file with the new name
        /// </summary>
        /// <param name="expressions">Expressions to search and replace</param>
        /// <exception cref="SaveException">Saving failure</exception>
        private protected abstract void Process(List<IExpression> expressions);
    }
}