using Gameplay.Player;
using UnityEngine;

public sealed class Enemy : MonoBehaviour
{
    [Header("Enemy Settings")]
    [SerializeField] private float _speed;
    [SerializeField] private float _radiusAgro;

    private Vector2 startPos;

    public IPlayer Player
    {
        get;
        set;
    }

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
        if (Player != null && !Player.IsDead && Vector2.Distance(transform.position, Player.Position) < _radiusAgro)
        {
            MoveTo(Player.Position);
        }
        else
        {
            MoveTo(startPos);
        }
    }

    private void MoveTo(Vector3 target)
    {
        transform.position = Vector2.MoveTowards(transform.position, target, _speed * Time.deltaTime);
    }

    #endregion
}