using System.IO;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Serilog;
using WisdomLight.Model;
using WisdomLight.Customing;
using static WisdomLight.Writers.AutoGenerating.AutoFiller;
using static WisdomLight.Writers.AutoGenerating.Processors;
using WisdomLight.ViewModel.Fields;

namespace WisdomLight.Writers.AutoGenerating.Documents
{
    public static class FileDocument
    {
        internal static void WriteDocuments
            (List<string> paths, List<IExpression> expressions, string saveTo)
        {
            for (byte i = 0; i < paths.Count; i++)
            {
                string templatePath = paths[i];

                if (!File.Exists(templatePath))
                    continue;

                string fileName = Path.GetFileName(templatePath);
                WriteDocument(templatePath, saveTo + fileName, expressions);
            }
        }

        private static void WriteDocument(string templatePath,
             string filePath, List<IExpression> expressions)
        {
            try
            {
                TruncateFile(filePath);
                FullProcessing(templatePath, filePath, expressions);
            }
            catch (IOException exception)
            {
                Log.Error("File processing exception: " + exception.Message);
                WriteMessage(exception.Message);
            }
        }

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

                    var body = template.MainDocumentPart.Document.Body;
                    var paragraphs = body.Elements<Paragraph>();
                    var cells = body.Descendants<TableCell>();

                    Process(paragraphs, cells, expressions);

                    using (StreamWriter sw = new StreamWriter(template.MainDocumentPart.GetStream(FileMode.Create)))
                    {
                        sw.Write(docText);
                    }
                }

                // Save the file with the new name
                Save(generatePath, stream);
            }
        }

        private static void Process(
            IEnumerable<Paragraph> paragraphs,
            IEnumerable<TableCell> cells,
            List<IExpression> expressions
            )
        {
            for (byte i = 0; i < expressions.Count; i++)
            {
                IExpression expression = expressions[i];

                string search = expression.Name;
                string replaceWith = expression.Value;

                ReplaceInParagraphs(paragraphs, search, replaceWith);
                ReplaceInCells(cells, search, replaceWith);
            }
        }
    }
}