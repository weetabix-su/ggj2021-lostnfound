using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class PlayerSFX : MonoBehaviour
{
    [Header("One-Shot Clips")]
    [SerializeField] AudioClip[] walkingClips;
    [SerializeField] AudioClip[] turningClips;
    [SerializeField] AudioClip[] jumpingClips;
    [SerializeField] AudioClip[] landingClips;

    AudioSource sfxSource;

    void OnEnable()
    {
        sfxSource = GetComponent<AudioSource>();
    }

    void playOneShot(AudioClip[] clips)
    {
        if (clips.Length <= 0) return;
        sfxSource.PlayOneShot(clips[Random.Range(0, clips.Length)]);
    }

    public void Walk() => playOneShot(walkingClips);
    public void Turn() => playOneShot(turningClips);
    public void Jump() => playOneShot(jumpingClips);
    public void Land() => playOneShot(landingClips);
}
