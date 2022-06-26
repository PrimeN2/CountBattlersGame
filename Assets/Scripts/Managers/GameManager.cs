﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : Singleton<GameManager>, IGameStateSwitcher
{
    public BaseGameState _currentState { get; private set; }
    private List<BaseGameState> _allStates; 

    [Header("UI Components")]
    [HideInInspector] public GameObject CurrentPanel;
    [SerializeField] private GameObject _mainMenuPanel;
    [SerializeField] private GameObject _gameMenuPanel;
    [SerializeField] private GameObject _loseMenuPanel;
    [SerializeField] private GameObject _wonMenuPanel;

    [Header("Player Components")]
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private ParticlesController _particlesController;
    [SerializeField] private PlayerAlliensHandler _playerAlliensHandler;
    [SerializeField] private Animator _animator;

    [Header("Spawners")]
    [SerializeField] private CharacterSpawner _characterSpawner;

    [Header("Input Components")]
    [SerializeField] private InputController _inputManager;

    private void Start()
    {
        StateArguments stateArguments = new StateArguments(_playerMovement, _inputManager, _particlesController, _animator);

        _allStates = new List<BaseGameState>()
        {
            new MainState(this, _mainMenuPanel, stateArguments),
            new PlayingState(this, _gameMenuPanel, stateArguments),
            new LostState(this, _loseMenuPanel, stateArguments),
            new FightState(this, stateArguments),
            new WonState(this, _wonMenuPanel, stateArguments)
        };
        _currentState = _allStates[0];

        _currentState.Load();
    }

    public void LoadMainMenu()
    {
        SwitchState<MainState>();
        _currentState.Load();
    }

    public void LoadGameHUD()
    {
        SwitchState<PlayingState>();
        _currentState.Load();
    }

    public void LoadLossMenu()
    {
        SwitchState<LostState>();
        _currentState.Load();
    }

    public void LoadWinMenu()
    {
        SwitchState<WonState>();
        _currentState.Load();
    }

    public void StartFight(Vector3 position)
    {
        SwitchState<FightState>();
        _currentState.Load();
    }

    public void SwitchState<T>() where T : BaseGameState
    {
        var state = _allStates.FirstOrDefault(s => s is T);
        _currentState = state;
    }

    private void OnEnable()
    {
        FinishHandler.OnFinished += LoadWinMenu;
        _characterSpawner.OnBunchTriggered += StartFight;
        _characterSpawner.OnBunchDefeated += LoadGameHUD;
        _playerAlliensHandler.OnPlayerLose += LoadLossMenu;
    }

    private void OnDisable()
    {
        FinishHandler.OnFinished -= LoadWinMenu;
        _characterSpawner.OnBunchTriggered -= StartFight;
        _characterSpawner.OnBunchDefeated -= LoadGameHUD;
        _playerAlliensHandler.OnPlayerLose -= LoadLossMenu;
    }
}

public class StateArguments
{
    public readonly PlayerMovement _playerMovement;
    public readonly InputController _inputManager;
    public readonly ParticlesController _particlesController;
    public readonly Animator _animatorController;


    public StateArguments(PlayerMovement playerMovement,
        InputController inputManager,
        ParticlesController particlesController,
        Animator animatorController)
    {
        _playerMovement = playerMovement;
        _inputManager = inputManager;
        _particlesController = particlesController;
        _animatorController = animatorController;
    }
}
