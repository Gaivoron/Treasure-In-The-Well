namespace Gameplay.Cameras
{
    public interface ICameraSettings
    {
        float PreviewSpeedNormal { get; }
        float PreviewSpeedAccelerated { get; }

        float PreviewPause { get; }

        ICameraBounds Bounds { get; }
    }
}