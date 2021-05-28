using Gameplay.Items;
using Gameplay.Player;
using System;

namespace Gameplay
{
    public sealed class Gameplay : IGameMode
    {
        public event Action<bool> Finished;

        private readonly HintText _monologueHint;
        private readonly ITimer _timer;
        private readonly ITimeController _timeController;
        private readonly IPortal _exit;
        private readonly IPlayer _player;

        public Gameplay(IPlayer player, HintText monologueHint, IPortal exit, ITimer timer, ITimeController timeController)
        {
            _exit = exit;
            _exit.Passed += OnPlayerPassed;

            _player = player;
            _player.ItemTaken += OnItemTaken;
            _player.Died += OnPlayerDied;

            _monologueHint = monologueHint;

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

        private void OnItemTaken(IItem item)
        {
            foreach (var key in item.Keys)
            {
                OnKeyObtained(key);
            }
        }

        private void OnKeyObtained(ItemKeys key)
        {
            switch (key)
            {
                case ItemKeys.QuestItem:
                    _monologueHint.ShowMoveUpHint();
                    break;

                case ItemKeys.Catalyst:
                    //TODO - raise threat level.
                    break;
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
            _player.ItemTaken -= OnItemTaken;
            _player.Died -= OnPlayerDied;
            _exit.Passed -= OnPlayerPassed;

            _timer.TimePassed -= OnTimePassed;

            Finished?.Invoke(hasWon);
        }
    }
}