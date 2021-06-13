using System;

namespace Gameplay
{
    public interface IGameMode
    {
        event Action<bool> Finished;
    }
}