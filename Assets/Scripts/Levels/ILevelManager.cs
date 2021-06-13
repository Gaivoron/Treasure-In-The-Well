using Profiles;
using System.Collections.Generic;

namespace Levels
{
    public interface ILevelManager
    {
        IGameLevel Current { get; }
        IEnumerable<IGameLevel> Levels { get; }

        void Load(ushort level);
    }
}
