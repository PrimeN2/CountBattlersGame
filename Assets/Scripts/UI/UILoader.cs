using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UILoader : MonoBehaviour, IGameStateSwitcher
{
    public BaseGameState _currentState { get; private set; }
    private List<BaseGameState> _allStates; 

    [Header("UI Components")]
    [SerializeField] private GameObject _mainMenuPanel;
    [SerializeField] private GameObject _pauseMenuPanel;
    [SerializeField] private GameObject _gameMenuPanel;
    [SerializeField] private GameObject _loseMenuPanel;

    [Header("Player Components")]
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private ParticlesController _particlesController;
    [SerializeField] private PlayerLife _playerLife;
    [SerializeField] private Animator _animator;

    [Header("Input Components")]
    [SerializeField] private InputManager _inputManager;

    private void Start()
    {
        _allStates = new List<BaseGameState>()
        {
            new MainState(this, _mainMenuPanel, _playerMovement, _inputManager, _particlesController),
            new PlayingState(this, _gameMenuPanel, _playerMovement, _inputManager, _particlesController),
            new PausedState(this, _pauseMenuPanel, _playerMovement, _inputManager, _particlesController),
            new LostState(this, _loseMenuPanel, _playerMovement, _inputManager, _particlesController)
        };
        _currentState = _allStates[0];

        LoadMainMenu();
    }

    public void LoadMainMenu()
    {
        _currentState.LoadMenu();
        //_animator.SetBool("IsMoving", false);
    }

    public void HideMenu()
    {
        _currentState.HideMenu();
        _currentState.LoadMenu();
        //_animator.SetBool("IsMoving", true);
    }

    public void LoadPauseMenu()
    {
        _currentState.HideMenu();
        _currentState.LoadMenu();
        _animator.SetBool("IsMoving", true);
    }
    public void LoadGameHUD()
    {
        _currentState.HideMenu();
        _currentState.LoadMenu();
        _animator.SetBool("IsMoving", true);
    }

    public void LoadLossMenu()
    {
        _currentState.HideMenu();
        _currentState.LoadMenu();
        _animator.SetBool("IsMoving", true);
    }

    public void SwitchState<T>() where T : BaseGameState
    {
        var state = _allStates.FirstOrDefault(s => s is T);
        _currentState = state;
    }

    private void OnEnable()
    {
        _playerLife.OnPlayerDied += HideMenu;
    }

    private void OnDisable()
    {
        _playerLife.OnPlayerDied -= HideMenu;
    }
}
