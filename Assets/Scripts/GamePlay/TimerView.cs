using UnityEngine;
using UnityEngine.UI;

namespace Gameplay
{
    public sealed class TimerView : MonoBehaviour, ITimerView
    {
        [SerializeField] private Text _timerText;

        private float _myTime = 0;

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
                _timerText.text = value.ToString("f2");
            }
        }

        public void Show(bool isVisible)
        {
            gameObject.SetActive(isVisible);
        }
    }
}