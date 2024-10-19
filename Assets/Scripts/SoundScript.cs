using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundScript : MonoBehaviour
{
    [SerializeField] private AudioClip SoundToPlay;
    private AudioSource audioSource;
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }
    public void PlayAudio(AudioClip sound = null)
    {
        if (sound == null)
            sound = SoundToPlay;

        audioSource.clip = sound;
        audioSource.Play();
    }
}