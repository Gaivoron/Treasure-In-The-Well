using Gameplay.Player;
using UnityEngine;

namespace Gameplay.Items
{
    public sealed class Ring : MonoBehaviour, IInteractable, IItem
    {
        void IInteractable.ApplyTo(IPlayer player)
        {
            if (player.TakeItem(this))
            {
                OnItemTaken();
            }
        }

        //TODO - move to a basic class.
        private void OnItemTaken()
        {
            AudioManager_script.Instance.RingSound();
            gameObject.SetActive(false);
        }
    }
}