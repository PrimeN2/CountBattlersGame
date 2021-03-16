using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
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
        Debug.Log(_playerControls.Touch.PrimaryPosition.ReadValue<Vector2>());
        OnTouchStarted?.Invoke(Utils.ScreenToWorld(_mainCamera, _playerControls.Touch.PrimaryPosition.ReadValue<Vector2>()));
    }
    private void EndTouchPrimary(InputAction.CallbackContext context)
    {
        Debug.Log(_playerControls.Touch.PrimaryPosition.ReadValue<Vector2>());
        OnTouchEnded?.Invoke(Utils.ScreenToWorld(_mainCamera, _playerControls.Touch.PrimaryPosition.ReadValue<Vector2>()));
    }
    public Vector2 PrimaryPosition()
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
