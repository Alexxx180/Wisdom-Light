using System.IO;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Serilog;
using WisdomLight.Model;
using WisdomLight.Customing;
using static WisdomLight.Writers.AutoGenerating.AutoFiller;
using static WisdomLight.Writers.AutoGenerating.Processors;

namespace WisdomLight.Writers.AutoGenerating.Documents
{
    public static class FileDocument
    {
        public static void WriteDocuments
            (Model.Document blanks, string saveTo)
        {
            for (byte i = 0; i < blanks.FileLocations.Count; i++)
            {
                string templatePath = blanks.FileLocations[i];

                if (!File.Exists(templatePath))
                    continue;

                System.Diagnostics.Trace.WriteLine("Proceed: " + templatePath);

                string fileName = Path.GetFileName(templatePath);

                System.Diagnostics.Trace.WriteLine("Saved to: " + saveTo + fileName);

                WriteDocument(templatePath,
                    saveTo + fileName, blanks.Information);
            }
        }

        private static void WriteDocument(string templatePath,
             string filePath, List<Expression> expressions)
        {
            try
            {
                TruncateFile(filePath);
                FullProcessing(templatePath, filePath, expressions);
            }
            catch (IOException exception)
            {
                Log.Error("Exception on file truncating/processing: " +
                    exception.Message);
                WriteMessage(exception.Message);
            }
        }

        private static void FullProcessing(string templatePath,
            string generatePath, List<Expression> expressions)
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
            List<Expression> expressions
            )
        {
            for (byte i = 0; i < expressions.Count; i++)
            {
                Expression expression = expressions[i];

                string search = expression.Name;
                string replaceWith = expression.Data;

                ReplaceInParagraphs(paragraphs, search, replaceWith);
                ReplaceInCells(cells, search, replaceWith);
            }
        }
    }
}