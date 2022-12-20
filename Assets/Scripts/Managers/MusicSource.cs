using UnityEngine;

public class MusicSource : MonoBehaviour
{
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private SessionData _sessionData;

    private void Start()
    {
        SetMusic(!_sessionData.IsMusicOn);
    }

    public void SetMusic(bool areOff)
    {
        _musicSource.mute = areOff;
    }

    private void OnEnable()
    {
        _sessionData.OnMusicToggled += SetMusic;
    }

    private void OnDisable()
    {
        _sessionData.OnMusicToggled -= SetMusic;
    }
}
