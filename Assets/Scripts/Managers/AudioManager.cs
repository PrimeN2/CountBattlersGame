using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private AudioSource _musicSource, _effectsSource;

    public void PlaySound(AudioClip clip)
    {
        _effectsSource.PlayOneShot(clip);
    }

    public void StartPlayingBackgroundSounds()
    {
        _musicSource.Play();
    }

    public void StopPlayingBackgroundSounds()
    {
        _musicSource.Stop();
    }
}
