using System.Collections.Generic;

namespace Gameplay.Items
{
    public interface IItem
    {
        int Value { get; }
        IEnumerable<ItemKeys> Keys { get; }
    }
}