using System;

namespace Menu
{
    public interface IOptionsWindow<T>
    {
        event Action<T> OptionChosen;
    }
}