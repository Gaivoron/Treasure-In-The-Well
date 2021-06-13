using System;

namespace Menu
{
    //TODO - should also implement IWindow interface.
    public sealed class Title : Window, IOptionsWindow<TitleOptions>
    {
        public event Action<TitleOptions> OptionChosen;

        public override void Show()
        {
            //TODO - setup buttons;
            base.Show();
        }

        public override void Close()
        {
            //TODO - release buttons.
            OptionChosen = null;
            base.Close();
        }

        //TODO - make private.
        public void Play() => OnOptionChosen(TitleOptions.PLAY);
        //TODO - make private.
        public void Exit() => OnOptionChosen(TitleOptions.EXIT);

        private void OnOptionChosen(TitleOptions option)
        {
            OptionChosen?.Invoke(option);
            Close();
        }
    }
}