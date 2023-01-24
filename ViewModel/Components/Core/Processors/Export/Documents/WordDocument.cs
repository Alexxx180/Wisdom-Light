using System.IO;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using WisdomLight.Model.Exceptions.IO;
using WisdomLight.ViewModel.Components.Data.Units.Fields;
using WisdomLight.ViewModel.Components.Core.Processors.Export.Units.Texts.Matching.Replace;
using WisdomLight.ViewModel.Components.Core.Processors.Export.Units;
using WisdomLight.ViewModel.Components.Core.Processors.Export.Texts.Matching.Replace;

namespace WisdomLight.ViewModel.Components.Core.Processors.Export.Documents
{
    internal class WordDocument : FileDocument
    {
        private string _template;
        private string _renderTo;

        public WordDocument()
        {
            _changer = new TextChanger();
            _extractors = new List<IParagraphsExtractor>
            {
                new ParagraphsExtractor()
            };
        }

        internal override FileDocument TemplateFrom(string template)
        {
            _template = template;
            return this;
        }

        internal override FileDocument GenerateTo(string template)
        {
            _renderTo = template;
            return this;
        }

        /// <summary>
        /// Process and save the WORD (.docx) file with the new name
        /// </summary>
        /// <param name="expressions">Expressions to search and replace</param>
        /// <exception cref="SaveException">Saving failure</exception>
        private protected override void Process(List<IExpression> expressions)
        {
            byte[] byteArray = File.ReadAllBytes(_template);

            using (MemoryStream stream = new MemoryStream())
            {
                stream.Write(byteArray, 0, byteArray.Length);
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

                    for (byte i = 0; i < _extractors.Count; i++)
                    {
                        foreach (Paragraph paragraph in _extractors[i].Extract(body))
                            _changer.Change(paragraph, "#Selderey#", "That's actually works");
                    }
                    
                    //ProcessAll(paragraphs, cells, expressions);

                    using (StreamWriter sw = new StreamWriter(template.MainDocumentPart.GetStream(FileMode.Create)))
                    {
                        sw.Write(docText);
                    }
                }

                // Save the file with the new name
                Save(_renderTo, stream.ToArray());
            }
        }

        //private static void ProcessAll(
        //    IEnumerable<Paragraph> paragraphs,
        //    IEnumerable<TableCell> cells,
        //    List<IExpression> expressions)
        //{
        //    for (byte i = 0; i < expressions.Count; i++)
        //    {
        //        IExpression expression = expressions[i];

        //        string search = expression.Name;
        //        string replaceWith = expression.Value;

        //        AutoFiller.ReplaceInParagraphs(paragraphs, search, replaceWith);
        //        AutoFiller.ReplaceInCells(cells, search, replaceWith);
        //    }
        //}

        private List<IParagraphsExtractor> _extractors;
        private TextChanger _changer;
    }
}
