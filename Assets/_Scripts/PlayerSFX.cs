using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSFX : MonoBehaviour
{
    [SerializeField] private AudioSource audioSrc;
    [SerializeField] private AudioClip jumpSFX;
    [SerializeField] private AudioClip dashSFX;
    [SerializeField] private AudioClip deathSFX;

    public void PlayJump()
    {
        audioSrc.PlayOneShot(jumpSFX);
    }

    public void PlayDashSFX()
    {
        audioSrc.PlayOneShot(dashSFX);
    }

    public void PlayDeathSFX()
    {
        audioSrc.PlayOneShot(deathSFX);
    }
}
