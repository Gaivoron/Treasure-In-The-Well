namespace AudioManagement
{
    public static class AudioManagerExtensions
    {
        public static ISound PlayMenuBackSound(this IAudioManager manager)
        {
            var sound = manager.Get("menu_back");
            sound?.Play();
            return sound;
        }

        public static ISound PlayVictorySound(this IAudioManager manager)
        {
            var sound = manager.Get("victory");
            sound?.Play();
            return sound;
        }

        public static ISound PlayDefeatSound(this IAudioManager manager)
        {
            var sound = manager.Get("defeat");
            sound?.Play();
            return sound;
        }

        public static ISound PlayEnemyAlertSound(this IAudioManager manager)
        {
            var sound = manager.Get("enemy_alert");
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

        public static ISound PlayLavaSplashSound(this IAudioManager manager)
        {
            var sound = manager.Get("lava_splash");
            sound?.Play();
            return sound;
        }

        public static ISound PlayInjurySound(this IAudioManager manager)
        {
            var sound = manager.Get("injury");
            sound?.Play();
            return sound;
        }

        public static ISound PlayFootSteps(this IAudioManager manager)
        {
            var sound = manager.Get("foot_steps");
            sound?.Play();
            return sound;
        }

        public static ISound PlayImpact(this IAudioManager manager)
        {
            var sound = manager.Get("impact");
            sound?.Play();
            return sound;
        }
    }
}