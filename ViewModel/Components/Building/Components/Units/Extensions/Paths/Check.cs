using System;
using System.IO;
using WisdomLight.Model.Exceptions.Argument;
using WisdomLight.ViewModel.Components.Core.Processors;

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
                isFile = false;
                Messages.Error(new BrokenExtensionException(exception, name));
            }

            return isFile;
        }
    }
}
