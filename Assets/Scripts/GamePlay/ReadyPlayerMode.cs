using UnityEngine;
using System;

namespace Gameplay
{
    public sealed class ReadyPlayerMode : IGameMode
    {
        private readonly ITimer _timer;
        private readonly IHintText _monologueHint;
        private readonly IHintText _inputHint;

        public event Action<bool> Finished;

        public ReadyPlayerMode(ITimer timer, IHintText inputHint, IHintText monologueHint)
        {
            _inputHint = inputHint;
            _inputHint.ShowStartHint();

            _monologueHint = monologueHint;
            _monologueHint.ShowPrepareHint();

            //TODO - let _inputHint take controll over input?
            _timer = timer;
            _timer.TimePassed += Update;
        }

        private void Update(float obj)
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                _timer.TimePassed -= Update;
                _inputHint.Hide();
                _monologueHint.Hide();
                Finished?.Invoke(true);
            }
        }
    }
}