using System.Collections.Generic;
using DocumentFormat.OpenXml.Wordprocessing;
using WisdomLight.Model;

namespace WisdomLight.ViewModel.Components.Core.Processors.Export.Replace.Texts.Matching
{
    public interface IMatcher
    {
        public Match IsMatch(IEnumerable<Text> texts, int textStart, int charStart, string find);
    }
}
