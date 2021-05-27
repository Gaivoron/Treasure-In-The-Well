using Gameplay.Player;
using UnityEngine;
using System.Collections.Generic;

namespace Gameplay.Items
{
    public sealed class Item : MonoBehaviour, IInteractable, IItem
    {
        [SerializeField] private ItemKeys[] _keys;

        IEnumerable<ItemKeys> IItem.Keys => _keys;

        void IInteractable.ApplyTo(IPlayer player)
        {
            if (player.TakeItem(this))
            {
                OnItemTaken();
            }
        }

        //TODO - move to a basic class?
        private void OnItemTaken()
        {
            AudioManager_script.Instance.RingSound();
            gameObject.SetActive(false);
        }
    }
}