using AudioManagement;
using UnityEngine;

namespace Gameplay
{
    public sealed class DefeatMode : GameOver
    {
        public DefeatMode(ITimer timer, IHintText inputHint, GameObject _gameOverText)
            : base(timer, inputHint)
        {
            AudioManager.Instance.PlayDefeatSound();
            _gameOverText.SetActive(true);
            RegisterTimer();
        }
    }
}