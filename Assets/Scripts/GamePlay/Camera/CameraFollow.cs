using UnityEngine;
namespace Gameplay.Cameras
{
    public sealed class CameraFollow : MonoBehaviour
    {
        public Transform Player;

        [SerializeField] private Vector3 posOffset;

        private Vector3 startPos;
        private Vector3 endPos;

        [SerializeField] private float timeOffset;
        [SerializeField] private CameraBounds _bounds;

        private void Update()
        {
            if (Player == null)
            {
                return;
            }

            startPos = transform.position;
            endPos = Player.position;

            endPos.x += posOffset.x;
            endPos.y += posOffset.y;
            endPos.z = -15;

            transform.position = Vector3.Lerp(startPos, endPos, timeOffset * Time.deltaTime);
            transform.position = _bounds.Clamp(transform.position);
        }
    }
}