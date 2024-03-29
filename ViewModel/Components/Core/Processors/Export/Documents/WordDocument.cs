﻿using System.IO;
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
        private List<ParagraphExtracting> _extracting;
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

        public override FileDocument Extract(List<ParagraphExtracting> options)
        {
            _extracting = options;
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

            try
            {
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

                        for (byte i = 0; i < _extracting.Count; i++)
                        {
                            foreach (Paragraph paragraph in Extractors[_extracting[i]].Extract(body))
                            {
                                for (int ii = 0; ii < fields.Count; ii++)
                                {
                                    IExpression current = fields[ii].Current;
                                    if (string.IsNullOrEmpty(current.Name))
                                        continue;
                                    _changer.Change(paragraph, current.Name, current.Value ?? string.Empty);
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
            catch (IOException exception)
            {
                Messages.Error(new SaveException(exception, _renderTo));
            }
        }

        public Dictionary<ParagraphExtracting, IParagraphsExtractor> Extractors { get; set; }
    }
}
