namespace Gameplay.Cameras
{
    public interface ICameraBounds
    {
        float MaxX { get; }
        float MaxY { get; }
        float MinX { get; }
        float MinY { get; }
    }
}