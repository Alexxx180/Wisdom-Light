using System.IO;

namespace WisdomLight.ViewModel.Components.Building.Extensions.Paths.Folders
{
    public static class Concatenation
    {
        public static string Close(this string path)
        {
            return $"{path}{Path.DirectorySeparatorChar}";
        }
    }
}
