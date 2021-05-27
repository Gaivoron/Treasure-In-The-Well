using System.Collections.Generic;

namespace Gameplay.Items
{
    public interface IItem
    {
        IEnumerable<ItemKeys> Keys { get; }
    }
}