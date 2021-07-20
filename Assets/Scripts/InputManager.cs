using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class InputManager : Singleton<InputManager>
{
    private PlayerControls _playerControls;
    private Camera _mainCamera;
    private bool _isFirstTouch = true;

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
        _isFirstTouch = true;
    }
    private void Start()
    {
        _playerControls.Touch.PrimaryContact.started += ctx => StartTouchPrimary();
        _playerControls.Touch.PrimaryContact.canceled += ctx => EndTouchPrimary();
    }
    private void Update()
    {
        if (_playerControls.Touch.PrimaryContact.triggered && _isFirstTouch)
        {
            OnTouchStarted?.Invoke(Utils.ScreenToWorld(_mainCamera, _playerControls.Touch.PrimaryPosition.ReadValue<Vector2>()));
            _isFirstTouch = false;
        }
    }

    private void StartTouchPrimary()
    {
        if (!_isFirstTouch)
        {
            OnTouchStarted?.Invoke(Utils.ScreenToWorld(_mainCamera, _playerControls.Touch.PrimaryPosition.ReadValue<Vector2>()));
        }
    }
    private void EndTouchPrimary()
    {
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
