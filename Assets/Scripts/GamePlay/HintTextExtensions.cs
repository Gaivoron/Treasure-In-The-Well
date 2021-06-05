namespace Gameplay
{
    public static class HintTextExtensions
    {
        public static void Show(this IHintText hint, string text)
        {
            hint.SetText(text);
            hint.Show(true);
        }

        public static void Hide(this IHintText hint)
        {
            hint.Show(false);
        }

        public static void ShowRestartHint(this IHintText hint) => hint.Show("Press  \"R\"  to  Restart");
        public static void ShowStartHint(this IHintText hint) => hint.Show("Press  \"SPACEBAR\"  to  Jump  down  the  well");
        public static void ShowSkipHint(this IHintText hint) => hint.Show("Hold  \"SPACEBAR\"  to  Skip  forward");

        public static void ShowInterludeHint(this IHintText hint) => hint.Show("Oh  no!  I've  dropped  my  MAGIC  RING  into  that  cursed  WELL!");
        public static void ShowPrepareHint(this IHintText hint) => hint.Show("HURRY UP  and  get  IT  for  me  from  the  bottom  of  the  WEEL.\nI  promise  to  REWARD  you  handsomely.");

        public static void ShowMoveUpHint(this IHintText hint) => hint.Show("Now  bring  it  back  to  me.\nAnd  be  QUICK..");
    }
}