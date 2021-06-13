using UnityEngine;
using System;

namespace Gameplay
{
    public abstract class GameOver : IGameMode
    {
        public event Action<bool> Finished;

        protected readonly ITimer _timer;
        protected readonly IHintText _hint;

        protected GameOver(ITimer timer, IHintText inputHint)
        {
            _timer = timer;
            _hint = inputHint;
        }

        protected void RegisterRestartTimer()
        {
            _hint.ShowRestartHint();
            _timer.TimePassed += OnRestartTimePassed;
        }

        protected virtual void Finish(bool doContinue)
        {
            _timer.TimePassed -= OnRestartTimePassed;
            Finished?.Invoke(doContinue);
        }

        private void OnRestartTimePassed(float time)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                _timer.TimePassed -= OnRestartTimePassed;
                Finish(false);
            }
        }
    }
}