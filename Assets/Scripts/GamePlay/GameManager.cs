using UnityEngine;
using UnityEngine.SceneManagement;
using Gameplay.Player;
using Gameplay.Cameras;
using AudioManagement;
using Levels;

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

        private ushort Level
        {
            get => LevelManager.Instance.Current?.Index ?? 0;
        }

        public void GoToMainMenu()
        {
            AudioManager.Instance.PlayMenuBackSound();
            //TODO - use LevelManager.Instance instead
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
                    GetGameOverMode(hasWon).OnFinished(ContinueGame);
                }

                IGameMode GetGameOverMode(bool hasWon)
                {
                    if (hasWon)
                        return new VictoryMode(_timer, _inputHint, _monologueHint, _winGameText, _rewardText, _timerText, player, Level);

                    return new DefeatMode(_timer, _inputHint, _monologueHint, _gameOverText, player);
                }

                void ContinueGame(bool doContinue)
                {
                    if (doContinue)
                    {
                        //TODO - use methods of ILevelManager.Current?
                        LevelManager.Instance.Load((ushort)(Level + 1));
                        return;
                    }

                    //TODO - use methods of ILevelManager?
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
            }
        }
    }
}