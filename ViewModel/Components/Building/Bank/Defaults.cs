using System;
using System.IO;

namespace WisdomLight.ViewModel.Components.Building.Bank
{
    public static class Defaults
    {
        public static string Runtime => Path.Combine(Environment.CurrentDirectory, "Resources", "Runtime");
    }
}
