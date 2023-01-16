using System.IO;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using WisdomLight.ViewModel.Customing;
using WisdomLight.ViewModel.Files.Fields;
using WisdomLight.Model.Exceptions.IO;

namespace WisdomLight.ViewModel.Data.Files.Processors.Export.Documents
{
    internal class FileDocument : Saver
    {
        internal void WriteDocuments(IList<DocumentLinker> paths,
            List<IExpression> expressions, string saveTo)
        {
            for (byte i = 0; i < paths.Count; i++)
            {
                string template = paths[i].Type;

                if (!File.Exists(template))
                    continue;

                string fullName = saveTo + Path.GetFileName(template);

                try
                {
                    FullProcessing(template, fullName, expressions);
                }
                catch (SaveException exception)
                {
                    DialogManager.Message(exception);
                }
            }
        }

        /// <summary>
        /// Process and aave the file with the new name
        /// </summary>
        /// <param name="templatePath">Path to original template</param>
        /// <param name="generatePath">Save result to</param>
        /// <param name="expressions"></param>
        /// <exception cref="SaveException">Saving failure</exception>
        private static void FullProcessing(string templatePath,
            string generatePath, List<IExpression> expressions)
        {
            byte[] byteArray = File.ReadAllBytes(templatePath);
            using (MemoryStream stream = new MemoryStream())
            {
                stream.Write(byteArray, 0, byteArray.Length.ToInt());
                using (WordprocessingDocument template = WordprocessingDocument.Open(stream, true))
                {
                    string docText = null;
                    using (StreamReader sr = new StreamReader(template.MainDocumentPart.GetStream()))
                    {
                        docText = sr.ReadToEnd();
                    }

                    Body body = template.MainDocumentPart.Document.Body;
                    IEnumerable<Paragraph> paragraphs = body.Elements<Paragraph>();
                    IEnumerable<TableCell> cells = body.Descendants<TableCell>();

                    Process(paragraphs, cells, expressions);

                    using (StreamWriter sw = new StreamWriter(template.MainDocumentPart.GetStream(FileMode.Create)))
                    {
                        sw.Write(docText);
                    }
                }

                // Save the file with the new name
                Save(generatePath, stream.ToArray());
            }
        }

        private static void Process(
            IEnumerable<Paragraph> paragraphs,
            IEnumerable<TableCell> cells,
            List<IExpression> expressions)
        {
            for (byte i = 0; i < expressions.Count; i++)
            {
                IExpression expression = expressions[i];

                string search = expression.Name;
                string replaceWith = expression.Value;

                AutoFiller.ReplaceInParagraphs(paragraphs, search, replaceWith);
                AutoFiller.ReplaceInCells(cells, search, replaceWith);
            }
        }
    }
}