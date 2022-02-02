using UnityEngine;

public class MainState : BaseGameState
{
    private GameObject _menuPanel;

    public MainState(IGameStateSwitcher stateSwitcher, GameObject menuPanel,
        PlayerMovement playerMovement,
        InputManager inputManager, ParticlesController _particlesController, Animator animatorController)
        : base(stateSwitcher, playerMovement, inputManager, _particlesController, animatorController)
    {
        _menuPanel = menuPanel;
    }

    public override void LoadMenu()
    {
        if (_currentPanel != null && _currentPanel != _menuPanel)
            _currentPanel.SetActive(false);
        else
            _currentPanel = _menuPanel;
        _menuPanel.SetActive(true);
        _currentPanel = _menuPanel;

        _playerMovement.StopMoving();
        _inputManager.DeInitInputHandle();
        _particlesController.ContinueParticles();
        _animatorController.SetBool(IS_MOVING, false);
    }
}
