using System;

namespace Menu
{
    public static class OptionsWindowExtensions
    {
        public static void OnOptionChosen<T>(this IOptionsWindow<T> window, Action<T> handler)
        {
            window.OptionChosen += OnOptionChosen;

            void OnOptionChosen(T option)
            {
                window.OptionChosen -= OnOptionChosen;
                handler?.Invoke(option);
            }
        }
    }
}