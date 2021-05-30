using Gameplay.Player;
using UnityEngine;
using System.Collections.Generic;

namespace Gameplay.Items
{
    public sealed class Item : MonoBehaviour, IInteractable, IItem
    {
        [SerializeField] private int _value;
        [SerializeField] private ItemKeys[] _keys;

        int IItem.Value => _value;
        IEnumerable<ItemKeys> IItem.Keys => _keys;

        void IInteractable.ApplyTo(IPlayer player)
        {
            if (player.TakeItem(this))
            {
                OnItemTaken();
            }
        }

        private void OnItemTaken()
        {
            gameObject.SetActive(false);
        }
    }
}