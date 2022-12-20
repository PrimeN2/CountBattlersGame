using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyingState : BaseGameState
{
    private GameObject _menuPanel;

    public BuyingState(IGameStateSwitcher stateSwitcher, GameObject menuPanel, StateArguments stateArguments)
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
        _stateArguments._inputManager.DeinitInputHandle(false);
        _stateArguments._playerLabel.HideLabel();

        SceneController.Instance.StartDelayedReloading();
        AudioManager.Instance.StopPlayingBackgroundSounds();
    }
}
