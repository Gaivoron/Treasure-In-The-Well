namespace AudioManagement
{
    public interface ISound
    {
        bool IsPlaying { get; }

        void Play(bool loop = false);
    }
}