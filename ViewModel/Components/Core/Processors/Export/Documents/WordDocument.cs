using System.IO;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using WisdomLight.Model.Exceptions.IO;
using WisdomLight.ViewModel.Components.Data.Units.Fields;
using WisdomLight.ViewModel.Components.Core.Processors.Export.Units.Texts.Extracting;
using WisdomLight.ViewModel.Components.Core.Processors.Export.Units.Texts.Matching.Change;
using WisdomLight.ViewModel.Components.Data.Units.Fields.Tools;

namespace WisdomLight.ViewModel.Components.Core.Processors.Export.Documents
{
    internal class WordDocument : FileDocument
    {
        private TextChanger _changer;
        private string _template;
        private string _renderTo;

        public WordDocument()
        {
            _changer = new TextChanger();
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
        /// <param name="fields">Fields to search and expressions to replace</param>
        /// <exception cref="SaveException">Saving failure</exception>
        private protected override void Process(IList<FieldSelector> fields)
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

                    for (byte i = 0; i < Extractors.Count; i++)
                    {
                        foreach (Paragraph paragraph in Extractors[i].Extract(body))
                        {
                            for (int ii = 0; ii < Extractors.Count; ii++)
                            {
                                IExpression current = fields[ii].Current;
                                _changer.Change(paragraph, current.Name, current.Value);
                            }
                        }
                    }
                    
                    using (StreamWriter sw = new StreamWriter(template.MainDocumentPart.GetStream(FileMode.Create)))
                    {
                        sw.Write(docText);
                    }
                }

                // Save the file with the new name
                Save(_renderTo, stream.ToArray());
            }
        }
        
        public List<IParagraphsExtractor> Extractors { get;
            set; }
    }
}
