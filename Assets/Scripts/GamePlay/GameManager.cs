using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Gameplay.Player;
using Gameplay.Cameras;
using System;

namespace Gameplay
{
    public sealed partial class GameManager : IInteractable, IPortal
    {
        public event Action<IPlayer> Passed;

        void IInteractable.ApplyTo(IPlayer player)
        {
            if (player.HasQuestItem)
            {
                Passed?.Invoke(player);
            }
        }
    }

    public sealed partial class GameManager : MonoBehaviour, ITimeController
    {
        [Header("Camera")]
        [SerializeField] private CameraFollow _camera;
        [SerializeField] private CameraBounds _bounds;
        [SerializeField] private float _previewSpeed = 20f;
        [SerializeField] private float _previewPause = 5f;

        [Header("UI Ref's")]
        [SerializeField] private GameObject _gameOverText;
        [SerializeField] private HintText _monologueHint;
        [SerializeField] private HintText _inputHint;
        [SerializeField] private GameObject _winGameText;
        [SerializeField] private Text _timerText;

        private float _myTime;

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
            _camera.enabled = true;
            _camera.Player = player.transform;
            foreach (var enemy in _enemies)
            {
                enemy.Player = player;
            }

            return player;
        }

        private void Awake()
        {
            _timerText.gameObject.SetActive(false);
            _camera.enabled = false;
            _gameOverText.SetActive(false);
            _monologueHint.Hide();
            _inputHint.Hide();
            _winGameText.SetActive(false);

            var timer = GetComponent<ITimer>();

            var previewMode = new LevelPreviewMode(timer, _inputHint, _monologueHint, _bounds.transform, _bounds, _previewSpeed);
            previewMode.OnFinished(OnLevelShown);

            void OnLevelShown(bool _)
            {
                var readyMode = new ReadyPlayerMode(timer, _inputHint, _monologueHint);
                readyMode.OnFinished(OnPlayerReady);
            }

            void OnPlayerReady(bool _)
            {
                var player = CreatePlayer();
                //TODO - wait for player to land.
                _timerText.gameObject.SetActive(true);
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