using UnityEngine;

public class LostState : BaseGameState
{
    private GameObject _menuPanel;

    public LostState(IGameStateSwitcher stateSwitcher, GameObject menuPanel, 
        PlayerMovement playerMovement, 
        InputManager inputManager, ParticlesController _particlesController) 
        : base(stateSwitcher, playerMovement, inputManager, _particlesController)
    {
        _menuPanel = menuPanel;
    }

    public override void HideMenu()
    {
        _menuPanel.SetActive(false);
        _stateSwitcher.SwitchState<PlayingState>();
    }

    public override void LoadMenu()
    {
        _menuPanel.SetActive(true);
        _playerMovement.StopMoving();
        _inputManager.DeInitInputHandle();
        _particlesController.RemoveParticles();
    }
}
