using AudioManagement;
using Gameplay.Player;
using UnityEngine;

namespace Gameplay
{
    public sealed class DefeatMode : GameOver
    {
        public DefeatMode(ITimer timer, IHintText inputHint, IHintText monologueHint, GameObject _gameOverText, IPlayer player)
            : base(timer, inputHint)
        {
            monologueHint.ShowDeathHint(player.HasQuestItem);
            AudioManager.Instance.PlayDefeatSound();
            _gameOverText.SetActive(true);
            RegisterRestartTimer();
        }
    }
}