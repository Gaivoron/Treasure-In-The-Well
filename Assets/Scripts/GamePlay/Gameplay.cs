using Gameplay.Player;
using System;

namespace Gameplay
{
    public sealed class Gameplay : IGameMode
    {
        public event Action<bool> Finished;

        private readonly ITimer _timer;
        private readonly ITimeController _timeController;
        private readonly IExit _exit;
        private readonly IPlayer _player;

        //TODO - replace PlayerController with interface.
        public Gameplay(IPlayer player, IExit exit, ITimer timer, ITimeController timeController)
        {
            _exit = exit;
            _exit.Passed += OnPlayerPassed;

            _player = player;
            _player.Died += OnPlayerDied;

            _timer = timer;
            _timer.TimePassed += OnTimePassed;

            _timeController = timeController;
            _timeController.Time = 0;
        }

        private void OnPlayerPassed(IPlayer player)
        {
            if (player == _player)
            {
                OnFinished(true);
            }
        }

        private void OnPlayerDied()
        {
            OnFinished(false);
        }

        private void OnTimePassed(float time)
        {
            _timeController.Time += time;
        }

        private void OnFinished(bool hasWon)
        {
            _player.Died -= OnPlayerDied;
            _exit.Passed -= OnPlayerPassed;

            _timer.TimePassed -= OnTimePassed;

            Finished?.Invoke(hasWon);
        }
    }
}