using System.Collections.Generic;
using WisdomLight.ViewModel.Components.Core.Processors.Export.Documents;
using WisdomLight.ViewModel.Components.Core.Processors.Export.Units.Texts.Extracting;

namespace WisdomLight.ViewModel.Components.Building.Bank
{
    public static class Export
    {
        public static List<FileDocument> Exporters()
        {
            return new List<FileDocument>
            {
                new WordDocument()
                {
                    Extractors = new Dictionary<ParagraphExtracting, IParagraphsExtractor>
                    {
                        { ParagraphExtracting.PLAIN, new ParagraphsExtractor() },
                        { ParagraphExtracting.CELLS, new CellsExtractor() }
                    }
                }
            };
        }
    }
}
