using UnityEngine;

namespace Gameplay.Player
{
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Player Settings")]
        [SerializeField] private float _radius = 0.5f;
        [SerializeField] private float moveSpeed = 5;

        [Header("Other Objects")]
        [SerializeField] private Transform groundPoint;
        [SerializeField] private LayerMask groundLayer;
        //TODO - should not be public.
        public Rigidbody2D _rb2d;

        public bool IsGrounded
        {
            get
            {
                Collider2D groundCheck = Physics2D.OverlapCircle(groundPoint.position, _radius, groundLayer);
                return groundCheck != null;
            }
        }

        public float JumpForce
        {
            set;
            get;
        }

        public void MovePlayer(Vector2 movement)
        {
            _rb2d.velocity = new Vector2(movement.x * moveSpeed, _rb2d.velocity.y);
        }

        public void JumpPlayer(Vector2 movement)
        {
            _rb2d.velocity = movement * JumpForce;
        }
    }
}