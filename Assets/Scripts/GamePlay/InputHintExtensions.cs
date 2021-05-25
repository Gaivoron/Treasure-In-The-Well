namespace Gameplay
{
    public static class HintTextExtensions
    {
        public static void ShowRestartHint(this HintText hint) => hint.Show("Press  \"R\"  to  Restart");
        public static void ShowStartHint(this HintText hint) => hint.Show("Press  \"SPACEBAR\"  to  Jump  down  the  well");
        public static void ShowSkipHint(this HintText hint) => hint.Show("Hold  \"SPACEBAR\"  to  Skip  forward");
    }
}