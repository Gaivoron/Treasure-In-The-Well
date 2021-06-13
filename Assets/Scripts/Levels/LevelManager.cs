using Profiles;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

namespace Levels
{
    public sealed class LevelManager : ILevelManager
    {
        private static ILevelManager _instance;

        public static ILevelManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new LevelManager();
                }

                return _instance;
            }
        }

        //TODO add level 2;
        private const ushort LastLevel = 2;

        public IGameLevel Current
        {
            get;
            private set;
        }

        public IEnumerable<IGameLevel> Levels
        {
            get
            {
                var levels = ProfileManager.Instance.Profile.Levels.Select<ILevel, IGameLevel>(any => new CompletedLevel(any));
                if (!levels.Any())
                {
                    levels = levels.Append(new UnlockedLevel(0));
                }
                else if (levels.Last().Index != LastLevel)
                {
                    levels = levels.Append(new UnlockedLevel((ushort)(levels.Last().Index + 1)));
                }

                return levels.ToArray();
            }
        }

        private LevelManager()
        {
        }

        void ILevelManager.Load(ushort level)
        {
            Current = Levels.LastOrDefault(any => any.Index == level);
            SceneManager.LoadScene($"GameplayLevel_{level:000}");
        }

        private sealed class UnlockedLevel : IGameLevel
        {
            public ushort Index { get; }

            public ILevel Stats => null;

            public UnlockedLevel(ushort index)
            {
                Index = index;
            }
        }

        private sealed class CompletedLevel : IGameLevel
        {
            private ILevel _stats;

            public ushort Index => Stats.Index;

            public ILevel Stats { get; }

            public CompletedLevel(ILevel stats)
            {
                Stats = stats;
            }
        }
    }
    public interface IGameLevel
    {
        ushort Index { get; }
        ILevel Stats { get; }
    }
}