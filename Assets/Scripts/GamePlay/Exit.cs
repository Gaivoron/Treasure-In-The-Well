using UnityEngine;
using Gameplay.Player;
using System;

namespace Gameplay
{
    public sealed class Exit : MonoBehaviour, IInteractable, IPortal
    {
        public event Action<IPlayer> Passed;

        void IInteractable.ApplyTo(IPlayer player)
        {
            if (player.HasQuestItem)
            {
                Passed?.Invoke(player);
            }
        }
    }
}