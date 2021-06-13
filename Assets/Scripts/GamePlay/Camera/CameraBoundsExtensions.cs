using UnityEngine;
namespace Gameplay.Cameras
{
    public static class CameraBoundsExtensions
    {
        public static Vector3 Clamp(this ICameraBounds bounds, Vector3 position)
        {
            return bounds == null ? position : new Vector3
                (
                    Mathf.Clamp(position.x, bounds.MinX, bounds.MaxX),
                    Mathf.Clamp(position.y, bounds.MinY, bounds.MaxY),
                    position.z
                );
        }
    }
}