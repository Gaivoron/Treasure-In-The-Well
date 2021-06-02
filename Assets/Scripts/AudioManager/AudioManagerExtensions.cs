namespace AudioManagement
{
    public static class AudioManagerExtensions
    {
        public static ISound PlayWinSound(this IAudioManager manager)
        {
            var sound = manager.Get("win");
            sound?.Play();
            return sound;
        }

        public static ISound PlayQuestItemSound(this IAudioManager manager)
        {
            var sound =  manager.Get("quest_item");
            sound?.Play();
            return sound;
        }

        public static ISound PlayValuableItemSound(this IAudioManager manager)
        {
            var sound = manager.Get("valuable_item");
            sound?.Play();
            return sound;
        }

        public static ISound PlayJumpSound(this IAudioManager manager)
        {
            var sound = manager.Get("jump");
            sound?.Play();
            return sound;
        }
        public static ISound PlayDeathSound(this IAudioManager manager)
        {
            var sound = manager.Get("death");
            sound?.Play();
            return sound;
        }

        public static ISound PlayFootSteps(this IAudioManager manager)
        {
            var sound = manager.Get("foot_steps");
            sound?.Play();
            return sound;
        }
    }
}