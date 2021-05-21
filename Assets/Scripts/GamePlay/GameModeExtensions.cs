using System;

namespace Gameplay
{
    public static class GameModeExtensions
    {
        public static IGameMode OnFinished(this IGameMode mode, Action<bool> handler)
        {
            if (mode != null)
            {
                mode.Finished += OnModeFinished;
            }

            return mode;

            void OnModeFinished(bool isSuccess)
            {
                mode.Finished += OnModeFinished;
                handler?.Invoke(isSuccess);
            }
        }
    }
}