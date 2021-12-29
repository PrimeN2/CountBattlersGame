using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public event Action<Vector2> OnTouchStarted;
    public event Action<Vector2> OnTouchEnded;

    private Camera _mainCamera;
    private bool _isPaused = true;
    private Vector2 _currentPosition;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (_isPaused)
            return;
        OnTouchStarted?.Invoke(Utils.ScreenToWorld(_mainCamera, eventData.position));
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.dragging)
        {
            _currentPosition = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (_isPaused)
            return;
        OnTouchEnded?.Invoke(Utils.ScreenToWorld(_mainCamera, eventData.position));
    }

    public Vector2 PrimaryPosition()
    {
        return Utils.ScreenToWorld(_mainCamera, _currentPosition);
    }

    public void InitInputHandle()
    {
        _mainCamera = Camera.main;
        _isPaused = false;
    }

    public void DeInitInputHandle()
    {
        _isPaused = true;
    }
}
