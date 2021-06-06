namespace Gameplay
{
    //TODO - use a localization manager of some sort.
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
        public static void ShowContinueHint(this IHintText hint) => hint.Show("Press  \"SPACEBAR\"  to  Continue");

        public static void ShowStartHint(this IHintText hint, bool isPressed)
        {
            var text = isPressed ? "Release  \"SPACEBAR\"  to  Jump  down  the  well" : "Press  \"SPACEBAR\"  to  Jump  down  the  well";
            hint.Show(text);
        }
        public static void ShowFastForwardHint(this IHintText hint) => hint.Show("Hold  \"SPACEBAR\"  to  FAST FORWARD");

        public static void ShowInterludePart1(this IHintText hint) => hint.Show("Oh  no!  I've  dropped  my  MAGIC  RING  into  that  CURSED WELL!");
        public static void ShowInterludePart2(this IHintText hint) => hint.Show("I  am  willing  to  pay  handsomely  for  it's  RETRIEVAL");
        public static void ShowPrepareHint(this IHintText hint) => hint.Show("Are  you  brave enough  to  take  this  task?");

        public static void ShowMoveUpHint(this IHintText hint) => hint.Show("Now  bring  it  back  to  me.\nAnd  be  QUICK...");

        public static void ShowRewardHint(this IHintText hint) => hint.Show("Let's  see  how  much  I  can  PAY  you  for  your  SERVICE...");

        public static void ShowDeathHint(this IHintText hint, bool hasRing)
        {
            var text = hasRing ? "Such  a  pity!  Your  were  SO  close" : "Ah.  You  have  not  even  made  it  halfway";
            hint.Show(text);
        }
    }
}