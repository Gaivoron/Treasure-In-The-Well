using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingScript : MonoBehaviour
{
    [Header("Script Ref's")]
    [SerializeField] private PlayerController playerController;
   
    [Header("GameObjects Ref's")]
    [SerializeField] private GameObject Trail1;
    [SerializeField] private GameObject Trail2;

    private bool tookRing = false;
    public bool TookRing
    {
        get => tookRing;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            tookRing = true;
           AudioManager_script.Instance.RingSound();
            playerController.playerMovement.JumpForce = 20;
            gameObject.SetActive(false);
             Trail1.SetActive(true);
             Trail2.SetActive(true);
        }
    }
}
