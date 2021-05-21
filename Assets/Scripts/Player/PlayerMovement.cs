﻿using UnityEngine;

//TODO - move to a namespace.
[RequireComponent(typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private float _radius = 0.5f;
    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private float jumpForce = 2;

    [Header("Other Objects")]
    [SerializeField] private Transform groundPoint;
    [SerializeField] private LayerMask groundLayer;
    //TODO - should not be public.
    public Rigidbody2D _rb2d;

    public float JumpForce
    {
        set => jumpForce = value;
    }

    public void MovePlayer(Vector2 movement)
    {
        _rb2d.velocity = new Vector2(movement.x * moveSpeed, _rb2d.velocity.y);
    }

    public void JumpPlayer(Vector2 movement)
    {
        _rb2d.velocity = movement * jumpForce;
    }

    public bool IsGrounded()
    {
        Collider2D groundCheck = Physics2D.OverlapCircle(groundPoint.position, _radius, groundLayer);
        return groundCheck != null;
    }
}
