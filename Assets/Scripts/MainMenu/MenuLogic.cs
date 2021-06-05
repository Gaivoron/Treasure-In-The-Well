using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu
{
    public sealed class MenuLogic : MonoBehaviour
    {
        //TODO - move to a seperate windows controller?
        [SerializeField] private Title _title;

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
            //TODO - check if player completed first level.
            SceneManager.LoadScene("Game");
        }

        private void Exit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}