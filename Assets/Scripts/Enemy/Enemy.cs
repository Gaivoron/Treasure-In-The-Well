using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform player;

    [Header("Enemy Settings")]
    [SerializeField] private float _speed;
    [SerializeField] private float _radiusAgro;

    [Header("Script Ref's")]
    [SerializeField] private PlayerController playerController;


    private Vector2 startPos;

    private void Awake()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        MovingLogic();
    }

    # region MovingLogic
    private void MovingLogic()
    {

        if (!playerController.IsDead)
        {
            if (Vector2.Distance(transform.position, player.position) < _radiusAgro)
            {
                MovingToPlayer();
            }
            else if (Vector2.Distance(transform.position, player.position) > _radiusAgro)
            {
                MovingToOrigPos();
            }
        }
        else
        {
            MovingToOrigPos();
        }
    }
    private void MovingToPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, _speed * Time.deltaTime);
    }


    private void MovingToOrigPos()
    {
        transform.position = Vector2.MoveTowards(transform.position, startPos, _speed * Time.deltaTime);
    }
    #endregion
}
