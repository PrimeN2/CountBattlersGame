using UnityEngine;
using UnityEngine.EventSystems;

public class MusicSettingView : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private GameObject _overlap;
    [SerializeField] private AudioClip _toggleSound;
    [SerializeField] private SessionData _sessionData;

    private void Start()
    {
        _overlap.SetActive(!_sessionData.IsMusicOn);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        AudioManager.Instance.PlaySound(_toggleSound);

        _sessionData.ToggleMusic();
        _overlap.gameObject.SetActive(!_sessionData.IsMusicOn);
    }
}
