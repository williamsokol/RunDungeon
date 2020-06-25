using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    private AudioSource musikPlayer;
    public int          trakNumber;
    public  AudioClip[] traks;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        musikPlayer = GetComponent<AudioSource>();
        trakNumber  = 0;
        musikPlayer.clip = traks[trakNumber];
        musikPlayer.Play();
    }

     void Update()
    {
        if (!musikPlayer.isPlaying & trakNumber < traks.Length)
        {
            trakNumber++;
            musikPlayer.clip = traks[trakNumber];
            musikPlayer.Play();
        }
        if (!musikPlayer.isPlaying & trakNumber == traks.Length)
        {
            trakNumber = 0;
        }
    }
}
