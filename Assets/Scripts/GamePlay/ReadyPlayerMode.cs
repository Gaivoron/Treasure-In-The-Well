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
        private readonly Transform _camera;
        private readonly ICameraBounds _bounds;
        private readonly float _speed;

        public LevelPreviewMode(ITimer timer, Transform camera, ICameraBounds bounds, float speed)
        {
            _timer = timer;
            _timer.TimePassed += MoveDown;

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
                Finished?.Invoke(true);
            }
        }

        private float GetSpeed() => Input.GetKey(KeyCode.Space) ? _speed * 2 : _speed;
    }

    public sealed class ReadyPlayerMode : IGameMode
    {
        private readonly ITimer _timer;
        private readonly InputHint _inputHint;

        public event Action<bool> Finished;

        public ReadyPlayerMode(ITimer timer, InputHint inputHint)
        {
            _inputHint = inputHint;
            _inputHint.ShowStartHint();

            //TODO - let input hint take controll over input?
            _timer = timer;
            _timer.TimePassed += Update;
        }

        private void Update(float obj)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                _timer.TimePassed -= Update;
                _inputHint.Hide();
                Finished?.Invoke(true);
            }
        }
    }
}