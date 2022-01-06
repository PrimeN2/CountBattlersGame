using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private PlayerMovement _playerMovement;

    private Camera _mainCamera;

    private Vector2 _previousPostion;
    private Vector2 _currentPosition;

    private bool _isPaused = true;


    public void OnBeginDrag(PointerEventData eventData)
    {
        if (_isPaused)
            return;

        _currentPosition = Utils.ScreenToWorld(_mainCamera, eventData.position);
        _previousPostion = _currentPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.dragging && !_isPaused)
        {
            _currentPosition = Utils.ScreenToWorld(_mainCamera, eventData.position);
            _playerMovement.TryMove((_currentPosition - _previousPostion).x);
            _previousPostion = _currentPosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData) { }


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