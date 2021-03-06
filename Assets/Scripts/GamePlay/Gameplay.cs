using AudioManagement;
using Gameplay.Items;
using Gameplay.Player;
using System;
using System.Linq;

namespace Gameplay
{
    public sealed class Gameplay : IGameMode
    {
        public event Action<bool> Finished;

        private readonly IHintText _monologueHint;
        private readonly ITimer _timer;
        private readonly ITimerView _timeController;
        private readonly IPortal _exit;
        private readonly IPlayer _player;
        private readonly IEnviromentalHazard _hazard;

        public Gameplay(IPlayer player, IEnviromentalHazard hazard, IHintText monologueHint, IPortal exit, ITimer timer, ITimerView timeController)
        {
            _exit = exit;
            _exit.Passed += OnPlayerPassed;

            _player = player;
            _player.ItemTaken += OnItemTaken;

            _hazard = hazard;

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
            PlaySound(item);

            foreach (var key in item.Keys)
            {
                OnKeyObtained(key);
            }
        }

        private void PlaySound(IItem item)
        {
            if (item.Keys.Any(key => key == ItemKeys.QuestItem))
            {
                AudioManager.Instance.PlayQuestItemSound();
            }
            else if (item.Value > 0)
            {
                AudioManager.Instance.PlayValuableItemSound();
            }
        }

        private void OnKeyObtained(ItemKeys key)
        {
            switch (key)
            {
                case ItemKeys.QuestItem:
                    //TODO - also need to hide that hint once player jumps up.
                    _monologueHint.ShowMoveUpHint();
                    break;

                case ItemKeys.Catalyst:
                    _hazard?.Activate();
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
            _monologueHint.Hide();
            _hazard?.Deactivate();

            _player.ItemTaken -= OnItemTaken;
            _player.Died -= OnPlayerDied;
            _exit.Passed -= OnPlayerPassed;

            _timer.TimePassed -= OnTimePassed;

            Finished?.Invoke(hasWon);
        }
    }
}