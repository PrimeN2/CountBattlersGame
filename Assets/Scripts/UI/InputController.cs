using UnityEngine;
using UnityEngine.EventSystems;

public class InputController : MonoBehaviour, IDragHandler, IPointerDownHandler
{
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private GameStateSwitcher gameStateSwitcher;

    private bool _isPaused = true;
    private bool _readyForStart = false;

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        if (_readyForStart && _isPaused)
            gameStateSwitcher.LoadGameHUD();
    }

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

    public void DeinitInputHandle(bool readyForStart)
    {
        _readyForStart = readyForStart;
        _isPaused = true;
    }
}