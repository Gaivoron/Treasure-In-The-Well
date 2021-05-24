using UnityEngine;
namespace Gameplay.Cameras
{
    public sealed class CameraBounds : MonoBehaviour, ICameraBounds
    {
        [SerializeField] private float _minX, _maxX, _minY, _maxY;

        public float MinX => _minX;
        public float MaxX => _maxX;
        public float MinY => _minY;
        public float MaxY => _maxY;
    }
}