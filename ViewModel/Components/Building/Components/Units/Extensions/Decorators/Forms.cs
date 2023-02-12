namespace WisdomLight.ViewModel.Components.Building.Extensions.Decorators
{
    public static class Forms
    {
        public static string Lists(this string text, string header, string message)
        {
            return $"{text}\n- {header}: {message}";
        }
    }
}
