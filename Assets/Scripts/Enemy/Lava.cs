using AudioManagement;
using Gameplay.Player;
using UnityEngine;

public sealed class Lava : MonoBehaviour, IInteractable
{
    void IInteractable.ApplyTo(IPlayer player)
    {
        AudioManager.Instance.PlayLavaSplashSound();
        while (player.Health > 0)
        {
            --player.Health;
        }
    }
}