using AudioManagement;
using UnityEngine;

namespace Gameplay.Player
{
    public class PlayerInput : MonoBehaviour
    {
        private Vector2 _movementX;
        private Vector2 _movementY;

        private PlayerMovement _playerMove;
        private PlayerController _playerController;

        private float timeJumpCounter;
        private float timeJump = 0.5f;
        private bool isJumping;

        private ISound _footsteps;

        private void Awake()
        {
            _playerMove = GetComponent<PlayerMovement>();
            _playerController = GetComponent<PlayerController>();
        }

        public void Update()
        {
            if (_playerController.IsDead)
                return;

            float x = Input.GetAxisRaw("Horizontal");
            _movementX = new Vector2(x, 0).normalized;

            float y = Input.GetAxis("Jump");
            _movementY = new Vector2(0, y).normalized;

            JumpInput();
        }

        private void FixedUpdate()
        {
            if (_footsteps != null && !_footsteps.IsPlaying)
            {
                _footsteps = null;
            }

            if (Mathf.RoundToInt(_movementX.x) != 0 && _playerMove.IsGrounded && _footsteps == null)
            {
                _footsteps = AudioManager.Instance.PlayFootSteps();
            }

            _playerMove.MovePlayer(_movementX);
            FlipPlayer();
        }

        private void FlipPlayer()
        {
            if (_playerMove._rb2d.velocity.x > 0)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else if (_playerMove._rb2d.velocity.x < 0)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
        }

        #region JumpInputSettings
        private void JumpInput()
        {
            if (Input.GetKeyDown(KeyCode.Space) && _playerMove.IsGrounded)
            {
                //TODO - move to some other place.
                AudioManager.Instance.PlayJumpSound();
                isJumping = true;
                timeJumpCounter = timeJump;
                _playerMove.JumpPlayer(_movementY);
            }

            if (Input.GetKey(KeyCode.Space) && isJumping)
            {
                if (timeJumpCounter > 0)
                {
                    _playerMove.JumpPlayer(_movementY);
                    timeJumpCounter -= Time.deltaTime;
                }
            }
            if (Input.GetKeyUp(KeyCode.Space))
                isJumping = false;
        }
        #endregion
    }
}