using UnityEngine;

public class MainState : BaseGameState
{
    private GameObject _menuPanel;

    public MainState(IGameStateSwitcher stateSwitcher, GameObject menuPanel, StateArguments stateArguments)
        : base(stateSwitcher, stateArguments)
    {
        _menuPanel = menuPanel;
    }

    public override void Load()
    {
        if (_currentPanel != null && _currentPanel != _menuPanel)
            _currentPanel.SetActive(false);

        _menuPanel.SetActive(true);
        _currentPanel = _menuPanel;

        _stateArguments._playerMovement.StopMoving();
        _stateArguments._inputManager.DeInitInputHandle();
        _stateArguments._particlesController.ContinueParticles();
        //_stateArguments._animatorController.SetBool(IS_MOVING, false);
    }
}
