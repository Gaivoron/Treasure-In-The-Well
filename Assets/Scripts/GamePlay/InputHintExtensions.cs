namespace Gameplay
{
    public static class HintTextExtensions
    {
        public static void ShowRestartHint(this HintText hint) => hint.Show("Press  \"R\"  to  Restart");
        public static void ShowStartHint(this HintText hint) => hint.Show("Press  \"SPACEBAR\"  to  Jump  down  the  well");
        public static void ShowSkipHint(this HintText hint) => hint.Show("Hold  \"SPACEBAR\"  to  Skip  forward");

        public static void ShowInterludeHint(this HintText hint) => hint.Show("Oh  no!  I've  dropped  my  MAGIC  RING  into  that  cursed  WELL!");
        public static void ShowPrepareeHint(this HintText hint) => hint.Show("HURRY UP  and  get  IT  for  me  from  the  bottom  of  the  WEEL.\nI  promise  to  REWARD  you handsomely.");
    }
}