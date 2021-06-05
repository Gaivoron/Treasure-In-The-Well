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
        private readonly HintText _inputHint;
        private readonly HintText _monologueHint;
        private readonly Transform _camera;
        private readonly ICameraSettings _settings;

        public LevelPreviewMode(ITimer timer, HintText inputHint, HintText monologueHint, Transform camera, ICameraSettings settings)
        {
            _timer = timer;
            _timer.TimePassed += MoveDown;

            _inputHint = inputHint;
            _inputHint.ShowSkipHint();

            _monologueHint = monologueHint;
            _monologueHint.ShowInterludeHint();

            _camera = camera;
            _settings = settings;

            var position = _camera.position;
            position.y = _settings.Bounds.MaxY;
            _camera.position = position;
        }

        private void MoveDown(float time)
        {
            var position = _camera.transform.position;
            position.y -= GetSpeed() * time;

            _camera.position = _settings.Bounds.Clamp(position);
            if (Mathf.Abs(_camera.position.y - _settings.Bounds.MinY) <= Epsilon)
            {
                _timer.TimePassed -= MoveDown;
                _timer.TimePassed += MoveUp;
            }
        }

        private void MoveUp(float time)
        {
            var position = _camera.transform.position;
            position.y += GetSpeed() * time;

            _camera.position = _settings.Bounds.Clamp(position);
            if (Mathf.Abs(_camera.position.y - _settings.Bounds.MaxY) <= Epsilon)
            {
                _timer.TimePassed -= MoveUp;
                _inputHint.Hide();
                _monologueHint.Hide();
                Finished?.Invoke(true);
            }
        }

        private float GetSpeed() => Input.GetKey(KeyCode.Space) ? _settings.PreviewSpeedAccelerated : _settings.PreviewSpeedNormal;
    }
}