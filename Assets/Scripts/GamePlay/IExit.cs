using Gameplay.Player;
using System;

namespace Gameplay
{
    public interface IExit
    {
        event Action<IPlayer> Passed;
    }
}