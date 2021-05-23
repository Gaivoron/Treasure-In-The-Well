using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Gameplay.Player;
using System;

namespace Gameplay
{
    public sealed partial class GameManager : IInteractable, IExit
    {
        public event Action<IPlayer> Passed;

        void IInteractable.ApplyTo(IPlayer player)
        {
            if (player.HasRing)
            {
                Passed?.Invoke(player);
            }
        }
    }

    public sealed partial class GameManager : MonoBehaviour, ITimeController
    {
        [Header("UI Ref's")]
        [SerializeField] private GameObject _gameOverText;
        [SerializeField] private InputHint _inputHint;
        [SerializeField] private GameObject _winGameText;
        [SerializeField] private Text _timerText;

        private float _myTime;

        [SerializeField] private CameraFollow _camera;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private PlayerController _playerPrefab;

        [SerializeField] private Enemy[] _enemies;

        //TODO - move implementation of ITimeController into a seperate class?
        float ITimeController.Time
        {
            get => _myTime;
            set
            {
                _myTime = value;
                _timerText.text = value.ToString("f2");
            }
        }

        public void GoToMainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }

        private IPlayer CreatePlayer()
        {
            var player = Instantiate(_playerPrefab, _spawnPoint.position, Quaternion.identity, null);
            _camera.Player = player.transform;
            foreach (var enemy in _enemies)
            {
                enemy.Player = player;
            }

            return player;
        }

        private void Awake()
        {
            _gameOverText.SetActive(false);
            _inputHint.Hide();
            _winGameText.SetActive(false);

            //TODO - wait for player to land.
            //TODO - obtain ITimer.
            var timer = GetComponent<ITimer>();

            var readyMode = new ReadyPlayerMode(timer, _inputHint);
            readyMode.OnFinished(OnPlayerReady);

            void OnPlayerReady(bool _)
            {
                var player = CreatePlayer();
                var gameplay = new Gameplay(player, this, timer, this);
                gameplay.OnFinished(OnGameOver);

                void OnGameOver(bool hasWon)
                {
                    _inputHint.ShowRestartHint();
                    if (!hasWon)
                    {
                        _gameOverText.SetActive(true);
                    }
                    else
                    {
                        _winGameText.SetActive(true);
                    }

                    new GameOver(timer).OnFinished(RestartGame);
                }

                void RestartGame(bool doRestart)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
            }
        }
    }
}