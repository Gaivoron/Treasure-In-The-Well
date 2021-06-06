using UnityEngine;
using AudioManagement;
using System.Collections;
using Gameplay.Player;
using System.Linq;
using Profiles;
using Levels;

namespace Gameplay
{
    public sealed class VictoryMode : GameOver
    {
        private readonly RewardView _rewardText;

        public VictoryMode(ITimer timer, IHintText inputHint, IHintText monologueHint, GameObject winGameText, RewardView rewardText, ITimerView timerView, IPlayer player, ushort level)
            : base(timer, inputHint)
        {
            _rewardText = rewardText;
            winGameText.SetActive(true);
            AudioManager.Instance.PlayVictorySound();
            CoroutineManager.Instance.StartCoroutine(ShowScoreRoutine(player, monologueHint, timerView, level));
        }

        private IEnumerator ShowScoreRoutine(IPlayer player, IHintText monologueHint, ITimerView timerView, ushort level)
        {
            monologueHint.ShowRewardHint();

            var itemsValue = player.Items.Sum(any => any.Value);
            var itemsCollected = player.Items.Count(any => any.Value > 0);
            player.Release();
            var totalTime = timerView.Time;
            var reward = CalculateReward(totalTime);
            ProfileManager.Instance.Profile.SetLevelData(level, (ulong)(reward + itemsValue), totalTime);
              yield return new WaitForSecondsRealtime(2);

            _rewardText.Reward = 0;
            var localDelta = 0f;
            while (timerView.Time < timerView.Limit)
            {
                yield return null;
                localDelta += Time.deltaTime * 5f;
                var totalLimit = timerView.Limit - totalTime;
                localDelta = Mathf.Clamp(localDelta, 0, totalLimit);
                timerView.Time += localDelta;
                if (timerView.Time > timerView.Limit)
                {
                    timerView.Time = timerView.Limit;
                }
                _rewardText.Reward = (int)(reward * localDelta / totalLimit);
                if (_rewardText.Reward > reward)
                {
                    _rewardText.Reward = reward;
                }
            }

            yield return null;
            _rewardText.Reward = reward;

            for (var i = 1; i <= itemsCollected; ++i)
            {
                yield return new WaitForSeconds(0.1f);
                _rewardText.Reward = reward + itemsValue * i / itemsCollected;
            }

            monologueHint.Hide();

            if (LevelManager.Instance.Levels.LastOrDefault()?.Stats != null)
            {
                RegisterRestartTimer();
            }
            else
            {
                RegisterContinueTimer();
            }

            int CalculateReward(float time)
            {
                var score = (int)((timerView.Limit - time) * 2);
                return score > 0 ? score : 0;
            }
        }

        private void RegisterContinueTimer()
        {
            _hint.ShowContinueHint();
            _timer.TimePassed += OnContineTimePassed;
        }

        private void OnContineTimePassed(float time)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Finish(true);
            }
        }

        protected override void Finish(bool doContinue)
        {
            _timer.TimePassed += OnContineTimePassed;
            base.Finish(doContinue);
        }
    }
}