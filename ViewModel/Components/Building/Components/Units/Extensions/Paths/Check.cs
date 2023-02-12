using System;
using System.IO;
using WisdomLight.Model.Exceptions.Argument;

namespace WisdomLight.ViewModel.Components.Building.Extensions.Paths
{
    public static class Check
    {
        public static bool IsFile(this string name, string format)
        {
            bool isFile;

            try
            {
                isFile = Path.GetExtension(name).ToLower() == $".{format}";
            }
            catch (ArgumentException exception)
            {
                throw new BrokenExtensionException(exception, name);
            }

            return isFile;
        }
    }
}
