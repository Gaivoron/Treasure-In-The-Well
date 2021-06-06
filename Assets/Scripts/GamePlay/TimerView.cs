using System;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay
{
    public sealed class TimerView : MonoBehaviour, ITimerView
    {
        [SerializeField] private Text _timerText;

        [Space]
        [SerializeField] private float _timeLimit;
        [SerializeField] private float _timeCritical;

        private float _myTime = 0;

        float ITimerView.Limit => _timeLimit;

        float ITimerView.Time
        {
            get => _myTime;
            set
            {
                if (value < 0)
                {
                    value = 0;
                }

                _myTime = value;
                _timerText.color = GetColor(value);
                _timerText.text = value.ToString("f2");
            }
        }

        public void Show(bool isVisible)
        {
            gameObject.SetActive(isVisible);
        }


        private Color GetColor(float value)
        {
            if (value >= _timeLimit)
            {
                return Color.red;
            }

            if (value >= _timeCritical)
            {
                return Color.yellow;
            }

            return Color.green;
        }
    }
}