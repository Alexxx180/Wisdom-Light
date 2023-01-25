using System.Collections.Generic;
using DocumentFormat.OpenXml.Wordprocessing;
using WisdomLight.ViewModel.Components.Core.Processors.Export.Units.Texts.Matching.Change.Replace;

namespace WisdomLight.ViewModel.Components.Core.Processors.Export.Units.Texts.Extracting
{
    public class ParagraphsExtractor : WrapReplacer, IParagraphsExtractor
    {
        public IEnumerable<Paragraph> Extract(Body body)
        {
            IEnumerable<Paragraph> paragraphs = body.Elements<Paragraph>();
            foreach (var i in paragraphs)
            {
                yield return i;
            }
        }
    }
}
