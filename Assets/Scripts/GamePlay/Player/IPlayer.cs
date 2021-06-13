using Gameplay.Items;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Player
{
    public interface IPlayer
    {
        event Action Died;
        event Action<IItem> ItemTaken;

        bool IsDead { get; }

        bool HasQuestItem { get; }

        int Health { get; set; }

        Vector2 Position { get; }

        IEnumerable<IItem> Items { get; }

        bool TakeItem(IItem item);
        void Release();
    }
}