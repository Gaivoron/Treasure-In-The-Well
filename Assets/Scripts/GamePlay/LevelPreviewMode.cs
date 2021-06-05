using UnityEngine;
using System;
using Gameplay.Cameras;

namespace Gameplay
{
    public sealed class LevelPreviewMode : IGameMode
    {
        private const float Epsilon = 0.0001f;

        public event Action<bool> Finished;

        private readonly ITimer _timer;
        private readonly IHintText _inputHint;
        private readonly IHintText _monologueHint;
        private readonly Transform _camera;
        private readonly ICameraSettings _settings;

        private float Speed => HasAccelerationInput ? _settings.PreviewSpeedAccelerated : _settings.PreviewSpeedNormal;

        private bool HasAccelerationInput => Input.GetKey(KeyCode.Space);

        public LevelPreviewMode(ITimer timer, IHintText inputHint, IHintText monologueHint, Transform camera, ICameraSettings settings)
        {
            _timer = timer;

            _inputHint = inputHint;
            _inputHint.ShowFastForwardHint();

            _monologueHint = monologueHint;

            _camera = camera;
            _settings = settings;

            var position = _camera.position;
            position.y = _settings.Bounds.MaxY;
            _camera.position = position;

            ShowDescention();
        }

        private void UpdateHintVisibility()
        {
            UpdateHintVisibility(!HasAccelerationInput);
        }

        private void UpdateHintVisibility(bool isVisible)
        {
            _inputHint.Show(isVisible);
        }

        private void MoveCamera(Vector3 delta)
        {
            _camera.position = _settings.Bounds.Clamp(_camera.position + delta);
        }

        private void ShowDescention()
        {
            _monologueHint.ShowInterludePart1();
            _timer.TimePassed += MoveDown;

            void MoveDown(float time)
            {
                UpdateHintVisibility();
                MoveCamera(Vector3.down * Speed * time);
                if (Mathf.Abs(_camera.position.y - _settings.Bounds.MinY) <= Epsilon)
                {
                    _timer.TimePassed -= MoveDown;
                    FocusOnTarget();
                }
            }
        }

        private void FocusOnTarget()
        {
            _monologueHint.Hide();
            UpdateHintVisibility(false);

            var duration = _settings.PreviewPause;
            if (duration <= 0)
            {
                ShowAcension();
                return;
            }

            _timer.TimePassed += Wait;

            void Wait(float time)
            {
                duration -= time;
                if (duration <= 0)
                {
                    _timer.TimePassed -= Wait;
                    ShowAcension();
                }
            }
        }

        private void ShowAcension()
        {
            _monologueHint.ShowInterludePart2();
            _timer.TimePassed += MoveUp;

            void MoveUp(float time)
            {
                UpdateHintVisibility();
                MoveCamera(Vector3.up * Speed * time);
                if (Mathf.Abs(_camera.position.y - _settings.Bounds.MaxY) <= Epsilon)
                {
                    _timer.TimePassed -= MoveUp;
                    Finish();
                }
            }
        }

        private void Finish()
        {
            _inputHint.Hide();
            _monologueHint.Hide();
            Finished?.Invoke(true);
        }
    }
}