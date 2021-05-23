using UnityEngine;
using System;

namespace Gameplay
{
    public sealed class ReadyPlayerMode : IGameMode
    {
        private readonly ITimer _timer;
        private readonly InputHint _inputHint;

        public event Action<bool> Finished;

        public ReadyPlayerMode(ITimer timer, InputHint inputHint)
        {
            _inputHint = inputHint;
            _inputHint.ShowStartHint();

            //TODO - let input hint take controll over input?
            _timer = timer;
            _timer.TimePassed += Update;
        }

        private void Update(float obj)
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                _timer.TimePassed -= Update;
                _inputHint.Hide();
                Finished?.Invoke(true);
            }
        }
    }
}