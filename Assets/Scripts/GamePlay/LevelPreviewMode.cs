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
        private readonly ICameraBounds _bounds;
        private readonly float _speed;

        public LevelPreviewMode(ITimer timer, HintText inputHint, HintText monologueHint, Transform camera, ICameraBounds bounds, float speed)
        {
            _timer = timer;
            _timer.TimePassed += MoveDown;

            _inputHint = inputHint;
            _inputHint.ShowSkipHint();

            _monologueHint = monologueHint;
            _monologueHint.ShowInterludeHint();

            _camera = camera;
            _bounds = bounds;

            _speed = speed;

            var position = _camera.position;
            position.y = _bounds.MaxY;
            _camera.position = position;
        }

        private void MoveDown(float time)
        {
            var position = _camera.transform.position;
            position.y -= GetSpeed() * time;

            _camera.position = _bounds.Clamp(position);
            if (Mathf.Abs(_camera.position.y - _bounds.MinY) <= Epsilon)
            {
                _timer.TimePassed -= MoveDown;
                _timer.TimePassed += MoveUp;
            }
        }

        private void MoveUp(float time)
        {
            var position = _camera.transform.position;
            position.y += GetSpeed() * time;

            _camera.position = _bounds.Clamp(position);
            if (Mathf.Abs(_camera.position.y - _bounds.MaxY) <= Epsilon)
            {
                _timer.TimePassed -= MoveUp;
                _inputHint.Hide();
                _monologueHint.Hide();
                Finished?.Invoke(true);
            }
        }

        private float GetSpeed() => Input.GetKey(KeyCode.Space) ? _speed * 2 : _speed;
    }
}