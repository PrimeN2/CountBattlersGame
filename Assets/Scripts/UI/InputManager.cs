using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public event Action<Vector2> OnTouchStarted;
    public event Action<Vector2> OnTouchEnded;

    [SerializeField] private UILoader _UILoader;

    private Camera _mainCamera;
    private bool _isPaused = true;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_isPaused)
            return;
        OnTouchStarted?.Invoke(Utils.ScreenToWorld(_mainCamera, eventData.position));
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (_isPaused)
            return;
        OnTouchEnded?.Invoke(Utils.ScreenToWorld(_mainCamera, eventData.position));
    }

    public Vector2 PrimaryPosition()
    {
        return Utils.ScreenToWorld(_mainCamera, Input.mousePosition);
    }

    private void InitInputHandle()
    {
        _mainCamera = Camera.main;
        _isPaused = false;
    }

    private void DeInitInputHandle()
    {
        _isPaused = true;
    }

    private void OnEnable()
    {
        _UILoader.OnMenuLoaded += DeInitInputHandle;
        _UILoader.OnMenuHided += InitInputHandle;
    }

    private void OnDisable()
    {
        _UILoader.OnMenuLoaded -= DeInitInputHandle;
        _UILoader.OnMenuHided -= InitInputHandle;
    }
}
