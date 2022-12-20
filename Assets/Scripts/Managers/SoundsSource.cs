using UnityEngine;

public class SoundsSource : MonoBehaviour
{ 
    [SerializeField] private AudioSource _soundsSource;
    [SerializeField] private SessionData _sessionData;

    private void Start()
    {
        SetSounds(!_sessionData.AreSoundsOn);
    }

    public void SetSounds(bool areOff)
    {
        _soundsSource.mute = areOff;
    }

    private void OnEnable()
    {
        _sessionData.OnSoundsToggled += SetSounds;
    }

    private void OnDisable()
    {
        _sessionData.OnSoundsToggled -= SetSounds;
    }
}
