using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Profiles
{
    [Serializable]
    public sealed class Profile : IProfile
    {
        public event Action Changed;

        [SerializeField] private List<Level> _levels = new List<Level>();

        IEnumerable<ILevel> IProfile.Levels => _levels.OrderBy(level => level.Index).ToArray();

        ulong IProfile.Coins
        {
            get
            {
                ulong sum = 0;
                foreach (var level in _levels)
                {
                    sum += level.Reward;
                }

                return sum;
            }
        }

        void IProfile.SetLevelData(ushort index, ulong reward, float time)
        {
            var level = _levels.FirstOrDefault(any => any.Index == index);
            if (level == null)
            {
                level = new Level() { Index = index, Time = time, Reward = reward };
                _levels.Add(level);
            }
            else
            {
                if (level.Time > time)
                {
                    level.Time = time;
                }
                level.Reward += reward;
            }

            Changed?.Invoke();
        }

        [Serializable]
        private class Level : ILevel
        {
            [SerializeField] private ushort _index;
            [SerializeField] private ulong _reward;
            [SerializeField] private float _time;

            public ushort Index
            {
                get => _index;
                set => _index = value;
            }

            public ulong Reward
            {
                get => _reward;
                set => _reward = value;
            }

            public float Time
            {
                get => _time;
                set => _time = value;
            }
        }
    }

}