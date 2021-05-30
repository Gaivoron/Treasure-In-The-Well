namespace AudioManagement
{
    public static class AudioManagerExtensions
    {
        public static void PlayWinSound(this IAudioManager manager)
        {
            manager.Get("win")?.Play();
        }

        public static void PlayQuestItemSound(this IAudioManager manager)
        {
            manager.Get("quest_item")?.Play();
        }

        public static void PlayValuableItemSound(this IAudioManager manager)
        {
            manager.Get("valuable_item")?.Play();
        }

        public static void PlayJumpSound(this IAudioManager manager)
        {
            manager.Get("jump")?.Play();
        }
        public static void PlayDeathSound(this IAudioManager manager)
        {
            manager.Get("death")?.Play();
        }
    }
}