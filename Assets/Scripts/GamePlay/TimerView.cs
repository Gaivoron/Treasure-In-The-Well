using UnityEngine;
using UnityEngine.UI;

namespace Gameplay
{
    public sealed class TimerView : MonoBehaviour, ITimeController
    {
        [SerializeField] private Text _timerText;

        private float _myTime = 0;

        float ITimeController.Time
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

        internal void Show(bool isVisible)
        {
            gameObject.SetActive(isVisible);
        }
    }
}