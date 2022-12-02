using System.Collections.Generic;
using UnityEngine;

public class MainState : BaseGameState
{
    private GameObject _menuPanel;
    private RoadSegmentSpawner _roadSegmentSpawner;

    public MainState(IGameStateSwitcher stateSwitcher, GameObject menuPanel, StateArguments stateArguments, RoadSegmentSpawner roadSegmentSpawner)
        : base(stateSwitcher, stateArguments)
    {
        _menuPanel = menuPanel;
        _roadSegmentSpawner = roadSegmentSpawner;
    }

    public override void Load()
    {
        if (_currentPanel != null && _currentPanel != _menuPanel)
            _currentPanel.SetActive(false);

        _menuPanel.SetActive(true);
        _currentPanel = _menuPanel;

        AudioManager.Instance.StopPlayingBackgroundSounds();

        _roadSegmentSpawner.Initialize();

        _stateArguments._playerMovement.StopMoving();
        _stateArguments._inputManager.DeInitInputHandle();
        _stateArguments._animationHandler.StartStandAnimation();
        _stateArguments._playerLabel.HideLabel();
    }
}
