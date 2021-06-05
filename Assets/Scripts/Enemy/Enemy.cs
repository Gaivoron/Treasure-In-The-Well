using Gameplay.Player;
using UnityEngine;
using System;

public sealed class Enemy : MonoBehaviour
{
    public event Action<bool> TargetObtained;

    [Header("Enemy Settings")]
    [SerializeField] private float _speed;
    [SerializeField] private float _radiusAgro;

    [Space]
    [SerializeField] private GameObject[] _trails;

    private Vector2 startPos;
    private bool _hasTarget;

    public IPlayer Player
    {
        get;
        set;
    }

    private bool HasTarget
    {
        get => _hasTarget;
        set
        {
            if (value != _hasTarget)
            {
                _hasTarget = value;
                TargetObtained?.Invoke(value);
            }
        }
    }

    private void Awake()
    {
        HasTarget = false;
        ShowTrails(false);
        startPos = transform.position;

        //TODO - move outside of class?
        TargetObtained += OnTargetObtained;
    }

    private void Update()
    {
        MovingLogic();
    }

    private void OnDestroy()
    {
        TargetObtained = null;
    }

    private void OnTargetObtained(bool hasTarget)
    {
        //TODO - play sound.
        ShowTrails(hasTarget);
    }

    private void ShowTrails(bool isVisible)
    {
        foreach (var trail in _trails)
        {
            trail.SetActive(isVisible);
        }
    }

    # region MovingLogic
    private void MovingLogic()
    {
        HasTarget = Player != null && !Player.IsDead && Vector2.Distance(transform.position, Player.Position) < _radiusAgro;
        MoveTo(HasTarget ? Player.Position : startPos);
    }

    private void MoveTo(Vector3 target)
    {
        transform.position = Vector2.MoveTowards(transform.position, target, _speed * Time.deltaTime);
    }

    #endregion
}