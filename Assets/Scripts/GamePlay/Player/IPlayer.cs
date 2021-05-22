using Gameplay.Items;
using System;
using UnityEngine;

namespace Gameplay.Player
{
    public interface IPlayer
    {
        event Action Died;

        bool IsDead { get; }

        bool HasRing { get; }

        int Health { get; set; }

        Vector2 Position { get; }

        bool TakeItem(IItem item);
    }
}