using Levels;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Menu
{
    public sealed class LevelSelection : Window, IOptionsWindow<ushort?>
    {
        public event Action<ushort?> OptionChosen;

        [SerializeField] private LevelView _levelPrefab;
        [SerializeField] private Transform _levelsContainer;

        private readonly IList<LevelView> _levels = new List<LevelView>();

        public override void Show()
        {
            foreach (var level in LevelManager.Instance.Levels)
            {
                var view = Instantiate(_levelPrefab, _levelsContainer);
                view.Setup(level);
                view.Selected += (index) => OnOptionChosen(index);
                _levels.Add(view);
            }

            base.Show();
        }

        public override void Close()
        {
            for (var i = _levels.Count - 1; i >= 0; --i)
            {
                _levels[i].Release();
            }
            _levels.Clear();

            OptionChosen = null;
            base.Close();
        }

        public void Exit()
        {
            OnOptionChosen(null);
        }

        private void OnOptionChosen(ushort? index)
        {
            OptionChosen?.Invoke(index);
            Close();
        }
    }
}