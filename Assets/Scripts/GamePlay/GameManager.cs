using UnityEngine;
using UnityEngine.SceneManagement;
using Gameplay.Player;
using Gameplay.Cameras;
using AudioManagement;
using System.Collections;
using System.Linq;

namespace Gameplay
{
    public sealed class GameManager : MonoBehaviour
    {
        [Header("Camera")]
        [SerializeField] private CameraFollow _camera;
        [SerializeField] private CameraBounds _bounds;

        [Header("UI Ref's")]
        [SerializeField] private Timer _timer;
        [SerializeField] private TimerView _timerText;
        [SerializeField] private RewardView _rewardText;
        [SerializeField] private GameObject _gameOverText;
        [SerializeField] private HintText _monologueHint;
        [SerializeField] private HintText _inputHint;
        [SerializeField] private GameObject _winGameText;

        //TODO - move to a seperate class?
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private PlayerController _playerPrefab;

        [SerializeField] private Exit _exit;
        [SerializeField] private EnviromentalHazard _hazard;
        [SerializeField] private Enemy[] _enemies;

        public void GoToMainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }

        private IPlayer CreatePlayer()
        {
            var player = Instantiate(_playerPrefab, _spawnPoint.position, Quaternion.identity, null);
            _camera.enabled = true;
            _camera.Player = player.transform;           

            return player;
        }

        private void SetPlayer(IPlayer player)
        {
            foreach (var enemy in _enemies)
            {
                enemy.Player = player;
            }
        }

        private void Awake()
        {
            _timerText.Show(false);
            _camera.enabled = false;
            _gameOverText.SetActive(false);
            _monologueHint.Hide();
            _inputHint.Hide();
            _winGameText.SetActive(false);

            var previewMode = new LevelPreviewMode(_timer, _inputHint, _monologueHint, Camera.main.transform, _bounds);
            previewMode.OnFinished(OnLevelShown);

            void OnLevelShown(bool _)
            {
                var readyMode = new ReadyPlayerMode(_timer, _inputHint, _monologueHint);
                readyMode.OnFinished(OnPlayerReady);
            }

            void OnPlayerReady(bool _)
            {
                var player = CreatePlayer();
                SetPlayer(player);
                //TODO - wait for player to land?
                _timerText.Show(true);
                var gameplay = new Gameplay(player, _hazard, _monologueHint, _exit, _timer, _timerText);
                gameplay.OnFinished(OnGameOver);

                void OnGameOver(bool hasWon)
                {
                    SetPlayer(null);

                    _inputHint.ShowRestartHint();
                    if (!hasWon)
                    {
                        _gameOverText.SetActive(true);
                    }
                    else
                    {
                        AudioManager.Instance.PlayWinSound();
                        _winGameText.SetActive(true);
                        StartCoroutine(ShowScoreRoutine(player, _timerText));

                        player.Release();
                    }

                    new GameOver(_timer).OnFinished(RestartGame);
                }

                void RestartGame(bool doRestart)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
            }
        }

        private IEnumerator ShowScoreRoutine(IPlayer player, ITimeController timer)
        {
            var itemsValue = player.Items.Sum(any => any.Value);
            var totalTime = timer.Time;
            var reward = CalculateReward(totalTime);
            yield return new WaitForSecondsRealtime(2);

            _rewardText.Reward = 0;
            while (timer.Time >= 0.0001f)
            {
                yield return null;
                var delta = Time.deltaTime * 10f;
                timer.Time -= delta;
                _rewardText.Reward += (int)(reward * delta / totalTime);
                if (_rewardText.Reward > reward)
                {
                    _rewardText.Reward = reward;
                }
            }

            var itemsCollected = player.Items.Count(any => any.Value > 0);
            for (var i = 1; i <= itemsCollected; ++i)
            {
                yield return null;
                _rewardText.Reward = reward + itemsValue * i / itemsCollected;
            }
        }

        //TODO - move to a different class?
        private int CalculateReward(float time)
        {
            var score = (int)((time - 41) * 10);
            return score > 0 ? score : 0;
        }
    }
}