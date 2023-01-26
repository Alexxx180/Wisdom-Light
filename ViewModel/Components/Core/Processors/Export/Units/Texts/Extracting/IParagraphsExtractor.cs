using DocumentFormat.OpenXml.Wordprocessing;
using System.Collections.Generic;

namespace WisdomLight.ViewModel.Components.Core.Processors.Export.Units.Texts.Extracting
{
    public interface IParagraphsExtractor
    {
        public IEnumerable<Paragraph> Extract(Body body);
    }
}
