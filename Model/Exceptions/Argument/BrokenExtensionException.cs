using System;
using WisdomLight.ViewModel.Components.Building.Extensions.Decorators;

namespace WisdomLight.Model.Exceptions.Argument
{
    public class BrokenExtensionException : ArgumentException, IDetails
    {
        public string FileName { get; set; }

        public BrokenExtensionException(ArgumentException exception, string name)
            : base(exception.Message)
        {
            FileName = name;
        }

        public string Details()
        {
            return "!Extension Error!"
                .Lists("About", FileName)
                .Lists("Description", Message);
        }
    }
}
