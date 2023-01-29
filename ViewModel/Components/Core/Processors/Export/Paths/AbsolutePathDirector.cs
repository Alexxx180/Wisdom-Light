namespace WisdomLight.ViewModel.Components.Core.Processors.Export.Paths
{
    public class AbsolutePathDirector : IPathDirector
    {
        public string DirectPath(string path) => path;
        public string RedirectPath(string path) => path;
    }
}
