using DocumentFormat.OpenXml.Wordprocessing;

namespace WisdomLight.ViewModel.Components.Core.Processors.Export.Units.Texts.Matching.Replace
{
    public interface ITextReplacer
    {
        public int Replace(Text text, int current, params string[] lines);
    }
}
