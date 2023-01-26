using System.Collections.Generic;
using DocumentFormat.OpenXml.Wordprocessing;
using WisdomLight.Model;

namespace WisdomLight.ViewModel.Components.Core.Processors.Export.Units.Texts.Matching
{
    public interface IMatcher
    {
        public Match IsMatch(IEnumerable<Text> texts, int element, int character, string find);
    }
}
