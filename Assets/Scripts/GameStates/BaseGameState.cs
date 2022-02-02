using System.Collections.Generic;
using UnityEngine;

public abstract class BaseGameState
{
    protected const string IS_MOVING = "IsMoving";
    protected const string ON_FINISHED= "OnFinished";

    protected static GameObject _currentPanel;

    protected readonly IGameStateSwitcher _stateSwitcher;
    protected readonly PlayerMovement _playerMovement;
    protected readonly InputManager _inputManager;
    protected readonly ParticlesController _particlesController;
    protected readonly Animator _animatorController;

    protected BaseGameState(IGameStateSwitcher stateSwitcher, 
        PlayerMovement playerMovement, 
        InputManager inputManager,
        ParticlesController particlesController,
        Animator animatorController)
    {
        _stateSwitcher = stateSwitcher;
        _playerMovement = playerMovement;
        _inputManager = inputManager;
        _particlesController = particlesController;
        _animatorController = animatorController;
    }

    public abstract void LoadMenu();
}
