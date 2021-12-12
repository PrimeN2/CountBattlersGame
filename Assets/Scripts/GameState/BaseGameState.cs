using UnityEngine;

public abstract class BaseGameState
{
    protected readonly IGameStateSwitcher _stateSwitcher;
    protected readonly PlayerMovement _playerMovement;
    protected readonly InputManager _inputManager;
    protected readonly ParticlesController _particlesController;

    protected BaseGameState(IGameStateSwitcher stateSwitcher, 
        PlayerMovement playerMovement, 
        InputManager inputManager,
        ParticlesController particlesController)
    {
        _stateSwitcher = stateSwitcher;
        _playerMovement = playerMovement;
        _inputManager = inputManager;
        _particlesController = particlesController;
    }

    public abstract void LoadMenu();

    public abstract void HideMenu();
}
