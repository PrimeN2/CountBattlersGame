using UnityEngine;

public class PlayingState : BaseGameState
{
    private GameObject _menuPanel;

    public PlayingState(IGameStateSwitcher stateSwitcher, GameObject menuPanel, StateArguments stateArguments)
        : base(stateSwitcher, stateArguments)
    {
        _menuPanel = menuPanel;
    }

    public override void Load()
    {
        if (_currentPanel != null && _currentPanel != _menuPanel)
        {
            _currentPanel.SetActive(false);
            
        }

        _menuPanel.SetActive(true);
        _currentPanel = _menuPanel;

        _stateArguments._playerMovement.ContinueMoving();
        _stateArguments._inputManager.InitInputHandle();
        _stateArguments._particlesController.ContinueParticles();
        //_stateArguments._animatorController.SetBool(IS_MOVING, true);
    }
}
