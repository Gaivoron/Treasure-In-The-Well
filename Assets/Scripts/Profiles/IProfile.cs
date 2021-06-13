using System;
using System.Collections.Generic;

namespace Profiles
{
    public interface IProfile
    {
        event Action Changed;

        ulong Coins { get; }
        IEnumerable<ILevel> Levels { get; }

        void SetLevelData(ushort index, ulong reward, float time);
    }

    public interface ILevel
    {
        ushort Index { get; }
        float Time { get; }
    }
}