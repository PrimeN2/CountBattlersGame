using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager>
{
    private PlayerControls _playerControls;
    private Camera _mainCamera;

    #region Events
    public delegate void StartTouch(Vector2 position);
    public event StartTouch OnTouchStarted;
    public delegate void EndTouch(Vector2 position);
    public event EndTouch OnTouchEnded;
    #endregion
    private void Awake()
    {
        _playerControls = new PlayerControls();
        _mainCamera = Camera.main;
    }
    private void Start()
    {
        _playerControls.Touch.PrimaryContact.started += ctx => StartTouchPrimary(ctx);
        _playerControls.Touch.PrimaryContact.canceled += ctx => EndTouchPrimary(ctx);
    }

    private void StartTouchPrimary(InputAction.CallbackContext context)
    {
        OnTouchStarted?.Invoke(Utils.ScreenToWorld(_mainCamera, _playerControls.Touch.PrimaryPosition.ReadValue<Vector2>()));
    }
    private void EndTouchPrimary(InputAction.CallbackContext context)
    {
        OnTouchEnded?.Invoke(Utils.ScreenToWorld(_mainCamera, _playerControls.Touch.PrimaryPosition.ReadValue<Vector2>()));
    }
    public Vector3 PrimaryPosition()
    {
        return Utils.ScreenToWorld(_mainCamera, _playerControls.Touch.PrimaryPosition.ReadValue<Vector2>());
    }
    private void OnEnable()
    {
        _playerControls.Enable();
    }
    private void OnDisable()
    {
        _playerControls.Disable();
    }
}
