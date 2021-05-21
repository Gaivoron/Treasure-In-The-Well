using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public event Action Died;

    private Animator myAnim;

    internal PlayerMovement playerMovement;

    public bool IsDead
    {
        get;
        private set;
    }

    private void Awake()
    {
        IsDead = false;

        myAnim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        RunAnimation();
        JumpAnimation();
    }

    #region Animations
    private void RunAnimation()
    {
         myAnim.SetFloat("MoveSpeed", playerMovement._rb2d.velocity.magnitude);
    }

    private void JumpAnimation()
    {
        if (playerMovement._rb2d.velocity.y != 0)
        {
            myAnim.SetBool("isJumping", true);
        }
        else
        {
            myAnim.SetBool("isJumping", false);
        }
    }
    #endregion

    #region CollisionCheck

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Spikes")|| col.gameObject.CompareTag("Enemy"))
        {
           PlayerDeath();
        }
    }

    #endregion

    public void PlayerDeath()
    {
        playerMovement._rb2d.bodyType = RigidbodyType2D.Static;
        AudioManager_script.Instance.HurtSound();
        myAnim.Play("Death");

        IsDead = true;
        Died?.Invoke();
    }

}
