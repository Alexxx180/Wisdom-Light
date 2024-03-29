﻿using System.Collections.Generic;
using DocumentFormat.OpenXml.Wordprocessing;
using WisdomLight.ViewModel.Components.Core.Processors.Export.Units.Texts.Matching.Change.Replace;

namespace WisdomLight.ViewModel.Components.Core.Processors.Export.Units.Texts.Extracting
{
    public class CellsExtractor : WrapReplacer, IParagraphsExtractor
    {
        public IEnumerable<Paragraph> Extract(Body body)
        {
            IEnumerable<TableCell> cells = body.Descendants<TableCell>();
            foreach (TableCell cell in cells)
            {
                foreach (var cellData in cell)
                {
                    if (cellData is Paragraph textParagraph)
                        yield return textParagraph;
                }
            }
        }
    }
}
