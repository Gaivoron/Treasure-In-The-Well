using Levels;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    public sealed class LevelView : MonoBehaviour
    {
        public event Action<ushort> Selected;

        [SerializeField] private Text _levelNumber;
        [SerializeField] private Text _bestTime;
        [SerializeField] private Button _button;

        private ushort _number;

        public void Setup(IGameLevel level)
        {
            _number = level.Index;
            _bestTime.text = level.Stats != null ? level.Stats.Time.ToString("f2") : string.Empty;

            _levelNumber.text = ((ushort)(_number + 1)).ToString();
            _button.onClick.AddListener(OnSelected);
        }

        public void Release()
        {
            Destroy(gameObject);
        }

        private void OnSelected()
        {
            Selected?.Invoke(_number);
        }

        private void OnDestroy()
        {
            Selected = null;
        }
    }
}