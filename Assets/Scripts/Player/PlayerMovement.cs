using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private float jumpForce = 2;
    public float JumpForce
    {
        set => jumpForce = value;
    }

    [Header("Other Objects")]
    [SerializeField] private Transform groundPoint;
    [SerializeField] private LayerMask groundLayer;
    public Rigidbody2D _rb2d;




    public void MovePlayer(Vector2 movement)
    {
        _rb2d.velocity = new Vector2(movement.x * moveSpeed, _rb2d.velocity.y);
    }

    public void JumpPlayer(Vector2 movement)
    {
        _rb2d.velocity = movement * jumpForce;
    }

    public bool isGrounded()
    {
        Collider2D groundCheck = Physics2D.OverlapCircle(groundPoint.position, 0.5f, groundLayer);
         if(groundCheck != null)
         {
            return true;
         }
        return false;
    }
}
