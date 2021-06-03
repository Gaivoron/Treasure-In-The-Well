using UnityEngine;
using TMPro;

namespace Gameplay
{
    public sealed class RewardView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;

        private int _reward = 0;

        //TODO - extract interface?
        public int Reward
        {
            get => _reward;
            set
            {
                if (value < 0)
                {
                    value = 0;
                }

                _reward = value;
                _text.text = $"Your reward is {_reward} coins";
            }
        }
    }
}