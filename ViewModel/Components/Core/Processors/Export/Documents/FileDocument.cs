using System.IO;
using System.Collections.Generic;
using WisdomLight.Model.Exceptions.IO;
using WisdomLight.ViewModel.Components.Data.Units.Fields.Tools;
using WisdomLight.ViewModel.Components.Core.Processors.Export.Units.Texts.Extracting;
using WisdomLight.ViewModel.Components.Data.Units.Collections;
using WisdomLight.ViewModel.Components.Core.Dialogs.Traditional.Manager;

namespace WisdomLight.ViewModel.Components.Core.Processors.Export.Documents
{
    public abstract class FileDocument : Saver, IDocument
    {
        public void Export(IDocuments documents, IList<FieldSelector> expressions, string folder)
        {
            foreach (string document in documents.GetNextDocument())
            {
                if (!File.Exists(document))
                    continue;

                string name = Path.GetFileName(document);
                string fullName = Path.Combine(folder, name);
                TemplateFrom(document).GenerateTo(fullName);

                try
                {
                    Process(expressions);
                }
                catch (SaveException exception)
                {
                    Messages.Error(exception);
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
        /// Add text searching options
        /// </summary>
        /// <param name="options">Paragraph extracting options</param>
        public abstract FileDocument Extract(List<ParagraphExtracting> options);

        /// <summary>
        /// Process and save the file with the new name
        /// </summary>
        /// <param name="expressions">Expressions to search and replace</param>
        /// <exception cref="SaveException">Saving failure</exception>
        private protected abstract void Process(IList<FieldSelector> expressions);
    }
}