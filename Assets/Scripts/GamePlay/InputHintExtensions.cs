namespace Gameplay
{
    public static class InputHintExtensions
    {
        public static void ShowRestartHint(this InputHint hint) => hint.Show("Press  \"R\"  to  Restart");
        public static void ShowStartHint(this InputHint hint) => hint.Show("Press  \"SPACEBAR\"  to  Jump  down  the  well");
        public static void ShowSkipHint(this InputHint hint) => hint.Show("Hold  \"SPACEBAR\"  to  Skip  forward");
    }
}