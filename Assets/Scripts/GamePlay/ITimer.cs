using System;

namespace Gameplay
{
    public interface ITimer
    {
        event Action<float> TimePassed;
    }
}