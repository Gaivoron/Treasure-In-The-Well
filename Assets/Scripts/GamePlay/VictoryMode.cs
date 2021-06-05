using UnityEngine;
using AudioManagement;
using System.Collections;
using Gameplay.Player;
using System.Linq;

namespace Gameplay
{
    public sealed class VictoryMode : GameOver
    {
        private readonly RewardView _rewardText;

        public VictoryMode(ITimer timer, IHintText inputHint, IHintText monologueHint, GameObject winGameText, RewardView rewardText, ITimerView timerView, IPlayer player)
            : base(timer, inputHint)
        {
            _rewardText = rewardText;
            winGameText.SetActive(true);
            AudioManager.Instance.PlayVictorySound();
            CoroutineManager.Instance.StartCoroutine(ShowScoreRoutine(player, monologueHint, timerView));
        }

        private IEnumerator ShowScoreRoutine(IPlayer player, IHintText monologueHint, ITimerView timerView)
        {
            monologueHint.ShowRewardHint();

            var itemsValue = player.Items.Sum(any => any.Value);
            var itemsCollected = player.Items.Count(any => any.Value > 0);
            player.Release();
            var totalTime = timerView.Time;
            var reward = CalculateReward(totalTime);
            yield return new WaitForSecondsRealtime(2);

            _rewardText.Reward = 0;
            while (timerView.Time >= 0.0001f)
            {
                yield return null;
                var delta = Time.deltaTime * 10f;
                timerView.Time -= delta;
                _rewardText.Reward += (int)(reward * delta / totalTime);
                if (_rewardText.Reward > reward)
                {
                    _rewardText.Reward = reward;
                }
            }

            for (var i = 1; i <= itemsCollected; ++i)
            {
                yield return new WaitForSeconds(0.1f);
                _rewardText.Reward = reward + itemsValue * i / itemsCollected;
            }

            monologueHint.Hide();
            RegisterTimer();
        }

        //TODO - move to a different class?
        private int CalculateReward(float time)
        {
            var score = (int)((45 - time) * 2);
            return score > 0 ? score : 0;
        }
    }
}