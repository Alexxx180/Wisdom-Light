namespace WisdomLight.ViewModel.Components.Building.Extensions.Paths.Files
{
    public static class Concatenation
    {
        public static string ToFile(this string path, string extension)
        {
            return $"{path}.{extension}";
        }
    }
}
