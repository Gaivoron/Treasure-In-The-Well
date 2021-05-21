using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Gameplay
{
    public sealed class GameManager : MonoBehaviour, ITimeController
    {
        [Header("UI Ref's")]
        [SerializeField] private GameObject _gameOverText;
        [SerializeField] private GameObject _restartGameText;
        [SerializeField] private GameObject _winGameText;
        [SerializeField] private Text _timerText;

        private float _myTime;
        private bool _hasWon = false;

        [SerializeField] private CameraFollow _camera;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private PlayerController _playerPrefab;
        [SerializeField] private RingScript _ringScript;

        float ITimeController.Time
        {
            get => _myTime;
            set
            {
                _myTime = value;
                _timerText.text = value.ToString("f2");
            }
        }

        private void Awake()
        {
            _gameOverText.SetActive(false);
            _restartGameText.SetActive(false);
            _winGameText.SetActive(false);

            //TODO - wait for player to land.
            //TODO - obtain ITimer.
            var timer = GetComponent<ITimer>();
            var player = Instantiate(_playerPrefab, _spawnPoint.position, Quaternion.identity, null);
            _camera.Player = player.transform;
            var gameplay = new Gameplay(player, timer, this);
            gameplay.OnFinished(OnGameOver);

            void OnGameOver(bool hasWon)
            {
                _restartGameText.SetActive(true);
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

            void RestartGame(bool _)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        public void GoToMainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }

        /*

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("Player") && _ringScript.TookRing)
            {
                OnPlayerWon();
            }
        }

        private void OnPlayerWon()
        {
            _hasWon = true;

            _playerController.playerMovement._rb2d.gravityScale = 0;
            AudioManager_script.Instance.WinSound();
        }
        */
    }
}