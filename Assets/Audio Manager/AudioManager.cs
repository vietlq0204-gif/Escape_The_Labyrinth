using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource musicAudioSource;
    public AudioSource vfxaudioClip;

    public AudioClip musicClip;
    public AudioClip musicClip2;
    public AudioClip winClip;
    // Start is called before the first frame update
    void Start()
    {
        musicAudioSource.clip = musicClip;
        musicAudioSource.clip = musicClip2;
        musicAudioSource.Play();
    }
}
