using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour, IDragHandler
{
    [SerializeField] private PlayerMovement _playerMovement;

    private bool _isPaused = true;

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.dragging && !_isPaused)
        {
            _playerMovement.TryMove(eventData.delta.x);
        }
    }

    public void InitInputHandle()
    {
        _isPaused = false;
    }

    public void DeInitInputHandle()
    {
        _isPaused = true;
    }
}