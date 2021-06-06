namespace Gameplay
{
    public interface ITimerView
    {
        float Time { get; set; }
        float Limit { get; }
    }
}