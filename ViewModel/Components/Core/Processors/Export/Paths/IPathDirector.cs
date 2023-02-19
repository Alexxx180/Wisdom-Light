namespace WisdomLight.ViewModel.Components.Core.Processors.Export.Paths
{
    public interface IPathDirector
    {
        public string DirectPath(string path);
        public string RedirectPath(string path);
    }
}
