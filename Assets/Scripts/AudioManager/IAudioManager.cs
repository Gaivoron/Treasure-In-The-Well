namespace AudioManagement
{
    public interface IAudioManager
    {
        ISound Get(string key);
    }
}