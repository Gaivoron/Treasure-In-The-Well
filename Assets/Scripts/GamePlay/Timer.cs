using UnityEngine;
using System;

namespace Gameplay
{
    public sealed class Timer : MonoBehaviour, ITimer
    {
        public event Action<float> TimePassed;

        private void Update()
        {
            TimePassed?.Invoke(Time.deltaTime);
        }
    }
}