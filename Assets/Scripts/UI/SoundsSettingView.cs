using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class SoundsSettingView : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private GameObject _overlap;
    [SerializeField] private AudioClip _toggleSound;
    [SerializeField] private SessionData _sessionData;

    private void Start()
    {
        _overlap.SetActive(!_sessionData.AreSoundsOn);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        AudioManager.Instance.PlaySound(_toggleSound);

        _sessionData.ToggleSounds();
        _overlap.gameObject.SetActive(!_sessionData.AreSoundsOn);
    }
}
