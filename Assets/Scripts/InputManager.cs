using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class InputManager : Singleton<InputManager>
{
    [SerializeField] private UILoader _menuLoader;

    private PlayerControls _playerControls;
    private Camera _mainCamera;
    private bool _isTouchStarted = false;

    #region Events
    public delegate void StartTouch(Vector2 position);
    public event StartTouch OnTouchStarted;
    public delegate void EndTouch(Vector2 position);
    public event EndTouch OnTouchEnded;
    #endregion

    private void Start()
    {
        _mainCamera = Camera.main;
        _isTouchStarted = false;
    }

    private void InitInputSystem()
    {
        _playerControls = new PlayerControls();
        _playerControls.Enable();
        _playerControls.Touch.PrimaryContact.started += ctx => StartTouchPrimary(ctx);
        _playerControls.Touch.PrimaryContact.canceled += ctx => EndTouchPrimary(ctx);
    }

    private void DeinitInputSystem()
    {
        _playerControls?.Disable();
        _playerControls = null;
    }


    private void StartTouchPrimary(InputAction.CallbackContext context)
    {
        Vector2 touchPosition = _playerControls.Touch.PrimaryPosition.ReadValue<Vector2>();
        if (touchPosition == Vector2.zero)
        {
            StartCoroutine(WaitForCorrectValue());
            return;
        }
        OnTouchStarted?.Invoke(Utils.ScreenToWorld(_mainCamera, touchPosition));
        _isTouchStarted = true;
    }

    private IEnumerator WaitForCorrectValue()
    {
        yield return new WaitForEndOfFrame();

        OnTouchStarted?.Invoke(Utils.ScreenToWorld(_mainCamera, _playerControls.Touch.PrimaryPosition.ReadValue<Vector2>()));
    }

    private void EndTouchPrimary(InputAction.CallbackContext context)
    {
        if (!_isTouchStarted)
            return;
        OnTouchEnded?.Invoke(Utils.ScreenToWorld(_mainCamera, _playerControls.Touch.PrimaryPosition.ReadValue<Vector2>()));
        _isTouchStarted = false;

    }
    public Vector2 PrimaryPosition()
    {
        return Utils.ScreenToWorld(_mainCamera, _playerControls.Touch.PrimaryPosition.ReadValue<Vector2>());
    }
    private void OnEnable()
    {
        _menuLoader.OnMenuHided += InitInputSystem;
        _menuLoader.OnMenuLoaded += DeinitInputSystem;

        if (_playerControls != null)
            _playerControls.Enable();
    }
    private void OnDisable()
    {
        _menuLoader.OnMenuHided -= InitInputSystem;
        _menuLoader.OnMenuLoaded -= DeinitInputSystem;

        if (_playerControls != null)
            _playerControls.Disable();
    }
}
