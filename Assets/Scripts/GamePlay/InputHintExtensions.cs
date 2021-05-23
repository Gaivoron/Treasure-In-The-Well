namespace Gameplay
{
    public static class InputHintExtensions
    {
        public static void ShowRestartHint(this InputHint hint) => hint.Show("press  \"R\"  to  Restart");
        public static void ShowStartHint(this InputHint hint) => hint.Show("press  \"SPACEBAR\"  to  Start");
    }
}