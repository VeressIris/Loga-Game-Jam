using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBirdSounds : MonoBehaviour
{
    [SerializeField] private AudioSource audioSrc;
    [SerializeField] private AudioClip clip1;
    [SerializeField] private AudioClip clip2;

    public void PlayChirp1()
    {
        audioSrc.PlayOneShot(clip1);
    }

    public void PlayChirp2()
    {
        audioSrc.PlayOneShot(clip2);
    }
}
