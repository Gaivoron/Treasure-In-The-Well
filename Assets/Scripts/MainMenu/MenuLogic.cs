using Levels;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu
{
    public sealed class MenuLogic : MonoBehaviour
    {
        //TODO - move to a seperate windows controller?
        [SerializeField] private Title _title;
        [SerializeField] private LevelSelection _selector;

        private void Start()
        {
            ShowTitle();
        }

        private void ShowTitle()
        {
            _title.Show();
            _title.OnOptionChosen(OnOptionChosen);
        }

        private void OnOptionChosen(TitleOptions option)
        {
            switch (option)
            {
                case TitleOptions.PLAY:
                    Play();
                    break;

                case TitleOptions.EXIT:
                    Exit();
                    break;

                default:
                    throw new NotImplementedException($"Unknown otion {option}");
            }
        }

        private void Play()
        {
            if (LevelManager.Instance.Levels.Last().Index == 0)
            {
                LevelManager.Instance.Load(0);
            }
            else
            {
                SelectLevelManualy();
            }
        }

        private void Exit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        private void SelectLevelManualy()
        {
            _selector.Show();
            _selector.OnOptionChosen(LevelSelected);
        }

        private void LevelSelected(ushort? level)
        {
            if (!level.HasValue)
            {
                ShowTitle();
                return;
            }

            LevelManager.Instance.Load(level.Value);
        }
    }
}