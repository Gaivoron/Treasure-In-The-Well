using Gameplay.Player;
using System;

namespace Gameplay
{
    public interface IPortal
    {
        event Action<IPlayer> Passed;
    }
}