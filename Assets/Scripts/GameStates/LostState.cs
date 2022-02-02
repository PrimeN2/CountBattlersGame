using UnityEngine;

public class LostState : BaseGameState
{
    private GameObject _menuPanel;

    public LostState(IGameStateSwitcher stateSwitcher, GameObject menuPanel, 
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
        _menuPanel.SetActive(true);
        _currentPanel = _menuPanel;

        _playerMovement.StopMoving();
        _inputManager.DeInitInputHandle();
        _animatorController.SetBool(IS_MOVING, false);
    }
}
