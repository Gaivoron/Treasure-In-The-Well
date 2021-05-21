using System;

namespace Gameplay
{
    public sealed class Gameplay : IGameMode
    {
        public event Action<bool> Finished;

        private readonly ITimer _timer;
        private readonly ITimeController _timeController;

        private readonly PlayerController _player;

        //TODO - replace PlayerController with interface.
        public Gameplay(PlayerController player, ITimer timer, ITimeController timeController)
        {
            _player = player;
            //TODO - handle win scenario.
            _player.Died += OnPlayerDied;

            _timer = timer;
            _timer.TimePassed += OnTimePassed;

            _timeController = timeController;
            _timeController.Time = 0;
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

            _timer.TimePassed -= OnTimePassed;

            Finished?.Invoke(hasWon);
        }
    }
}