using UnityEngine;

public class PlayingState : BaseGameState
{
    private GameObject _menuPanel;

    public PlayingState(IGameStateSwitcher stateSwitcher, GameObject menuPanel,
        PlayerMovement playerMovement,
        InputManager inputManager, ParticlesController _particlesController, Animator animatorController)
        : base(stateSwitcher, playerMovement, inputManager, _particlesController, animatorController)
    {
        _menuPanel = menuPanel;
    }

    public override void LoadMenu()
    {
        if (_currentPanel != null && _currentPanel != _menuPanel)
        {
            _currentPanel.SetActive(false);
            
        }

        _menuPanel.SetActive(true);
        _currentPanel = _menuPanel;

        _playerMovement.ContinueMoving();
        _inputManager.InitInputHandle();
        _particlesController.ContinueParticles();
        _animatorController.SetBool(IS_MOVING, true);
    }
}
