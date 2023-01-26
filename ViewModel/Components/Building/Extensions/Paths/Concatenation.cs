namespace WisdomLight.ViewModel.Components.Building.Extensions.Paths
{
    public static class Concatenation
    {
        public static string ToPath(this string name, string path)
        {
            return $"{path}{name}";
        }
    }
}