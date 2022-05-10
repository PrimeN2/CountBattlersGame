using UnityEngine;

public class WonState : BaseGameState
{
    private GameObject _menuPanel;

    public WonState(IGameStateSwitcher stateSwitcher, GameObject menuPanel, StateArguments stateArguments)
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
        //_stateArguments._animatorController.SetTrigger(ON_FINISHED);
    }
}
