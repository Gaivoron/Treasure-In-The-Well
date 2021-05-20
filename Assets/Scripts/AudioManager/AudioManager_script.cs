using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager_script : MonoBehaviour
{
    public static AudioManager_script Instance;

    [Header("Audio Clips")]
    [SerializeField] private AudioSource[] PlayerHurt;
    [SerializeField] private AudioSource Music;
    [SerializeField] private AudioSource Jump;
    [SerializeField] private AudioSource RingPickUp;
    [SerializeField] private AudioSource Win;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    private void Start()
    {
        DontDestroyOnLoad(Music);
        MusicPlay();

    }

    public void HurtSound()
    {
        var random = Random.Range(0, PlayerHurt.Length);
        PlayerHurt[random].Play();
    }

    public void JumpSound()
    {
        Jump.pitch = Random.Range(1, 1.6f);
        Jump.Play();
    }

    public void RingSound() => RingPickUp.Play();

    public void WinSound() => Win.Play();

    private void MusicPlay()
    {
        Music.playOnAwake = false;
    }

   
    
     

}
