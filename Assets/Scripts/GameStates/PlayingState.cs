using UnityEngine;

public class PlayingState : BaseGameState
{
    private GameObject _menuPanel;

    public PlayingState(IGameStateSwitcher stateSwitcher, GameObject menuPanel,
        PlayerMovement playerMovement,
        InputManager inputManager, ParticlesController _particlesController)
        : base(stateSwitcher, playerMovement, inputManager, _particlesController)
    {
        _menuPanel = menuPanel;
    }

    public override void HideMenu()
    {
        _menuPanel.SetActive(false);
        if (PlayerLife.IsPlayerDead)
            _stateSwitcher.SwitchState<LostState>();
        else
            _stateSwitcher.SwitchState<PausedState>();
    }

    public override void LoadMenu()
    {
        if (_menuPanel.activeInHierarchy)
        {
            _stateSwitcher.SwitchState<PausedState>();
            ((UILoader)_stateSwitcher).LoadMenu();
        }
        _menuPanel.SetActive(true);
        _playerMovement.ContinueMoving();
        _inputManager.InitInputHandle();
        _particlesController.ContinueParticles();
    }
}
