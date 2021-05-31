using Gameplay.Player;
using UnityEngine;

public sealed class Lava : MonoBehaviour, IInteractable
{
    void IInteractable.ApplyTo(IPlayer player)
    {
        while (player.Health > 0)
        {
            --player.Health;
        }
    }
}