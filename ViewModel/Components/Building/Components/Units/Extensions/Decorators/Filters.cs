namespace WisdomLight.ViewModel.Components.Building.Extensions.Decorators
{
    public static class Filters
    {
        public static string Option(string header, string type)
        {
            return $"{header} ({type})|{type}";
        }

        public static string Option(this string text, string header, string type)
        {
            return $"{text}|{header} ({type})|{type}";
        }
    }
}
