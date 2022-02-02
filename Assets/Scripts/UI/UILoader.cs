using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UILoader : MonoBehaviour, IGameStateSwitcher
{
    public BaseGameState _currentState { get; private set; }
    private List<BaseGameState> _allStates; 

    [Header("UI Components")]
    [HideInInspector] public GameObject CurrentPanel;
    [SerializeField] private GameObject _mainMenuPanel;
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
            new MainState(this, _mainMenuPanel, _playerMovement, _inputManager, _particlesController, _animator),
            new PlayingState(this, _gameMenuPanel, _playerMovement, _inputManager, _particlesController, _animator),
            new LostState(this, _loseMenuPanel, _playerMovement, _inputManager, _particlesController, _animator),
            new WonState(this, _gameMenuPanel, _playerMovement, _inputManager, _particlesController, _animator)
        };
        _currentState = _allStates[0];

        LoadMainMenu();
    }

    public void LoadMainMenu()
    {
        SwitchState<MainState>();
        _currentState.LoadMenu();
    }

    public void LoadGameHUD()
    {
        SwitchState<PlayingState>();
        _currentState.LoadMenu();
    }

    public void LoadLossMenu()
    {
        SwitchState<LostState>();
        _currentState.LoadMenu();
    }

    public void LoadWonMenu()
    {
        SwitchState<WonState>();
        _currentState.LoadMenu();
    }

    public void SwitchState<T>() where T : BaseGameState
    {
        var state = _allStates.FirstOrDefault(s => s is T);
        _currentState = state;
    }

    private void OnEnable()
    {
        _playerLife.OnPlayerDied += LoadLossMenu;
    }

    private void OnDisable()
    {
        _playerLife.OnPlayerDied -= LoadLossMenu;
    }
}
