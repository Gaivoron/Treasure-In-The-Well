using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Player;

    [SerializeField] private Vector3 posOffset;

    private Vector3 startPos;
    private Vector3 endPos;

    [SerializeField] private float timeOffset;
    [SerializeField] private float leftLimit, rightLimit, topLimit, botLimit;


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
        LimitCamera();
    }

    private void LimitCamera()
    {
        transform.position = new Vector3
            (
                Mathf.Clamp(transform.position.x, leftLimit, rightLimit),
                Mathf.Clamp(transform.position.y, botLimit, topLimit),
                transform.position.z
            );
    }
}
