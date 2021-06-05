using UnityEngine;
namespace Gameplay.Cameras
{
    public sealed class CameraBounds : MonoBehaviour, ICameraSettings, ICameraBounds
    {
        [SerializeField] private float _previewSpeed;
        [SerializeField] private float _previewAcceleration;
        [SerializeField] private float _previewPause;

        [Space]
        [SerializeField] private float _minX, _maxX, _minY, _maxY;

        float ICameraSettings.PreviewSpeedNormal => _previewSpeed;
        float ICameraSettings.PreviewSpeedAccelerated => _previewSpeed * _previewAcceleration;

        float ICameraSettings.PreviewPause => _previewPause;

        ICameraBounds ICameraSettings.Bounds => this;

        //TODO - move to a seperate class?
        float ICameraBounds.MinX => _minX;
        float ICameraBounds.MaxX => _maxX;
        float ICameraBounds.MinY => _minY;
        float ICameraBounds.MaxY => _maxY;
    }
}