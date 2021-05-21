using UnityEngine;
using System;

namespace Gameplay
{
    public sealed class GameOver : IGameMode
    {
        public event Action<bool> Finished;

        private readonly ITimer _timer;

        public GameOver(ITimer timer)
        {
            _timer = timer;
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