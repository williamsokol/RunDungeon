using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour
{
    private AudioSource soundPlayer;
    public AudioClip[] Sound;
    // Start is called before the first frame update
    void Awake()
    {
        soundPlayer = GetComponent<AudioSource>();

    }
    public void AttackSFX()
    {
        soundPlayer.clip = Sound[0];
        soundPlayer.Play();
       // print("test");
    }
    public void AttackedSFX()
    {
        soundPlayer.clip = Sound[1];
        soundPlayer.Play();
        //print("test");
    }

    public void WalkSFX()
    {
        soundPlayer.clip = Sound[2];
        soundPlayer.Play();
    }
    
}
