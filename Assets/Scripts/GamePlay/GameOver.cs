using UnityEngine;
using System;

namespace Gameplay
{
    public abstract class GameOver : IGameMode
    {
        public event Action<bool> Finished;

        private readonly ITimer _timer;
        private readonly IHintText _hint;

        protected GameOver(ITimer timer, IHintText inputHint)
        {
            _timer = timer;
            _hint = inputHint;
        }

        protected void RegisterTimer()
        {
            _hint.ShowRestartHint();
            _timer.TimePassed += OnTimePassed;
        }

        private void OnTimePassed(float time)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Finished?.Invoke(true);
            }
        }
    }
}