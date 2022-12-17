using UnityEngine;

public class MainState : BaseGameState
{
    private GameObject _menuPanel;
    private RoadSetter _platformSetter;

    public MainState(IGameStateSwitcher stateSwitcher, GameObject menuPanel, StateArguments stateArguments, RoadSetter platformSetter)
        : base(stateSwitcher, stateArguments)
    {
        _menuPanel = menuPanel;
        _platformSetter = platformSetter;
    }

    public override void Load()
    {
        if (_currentPanel != null && _currentPanel != _menuPanel)
            _currentPanel.SetActive(false);

        _menuPanel.SetActive(true);
        _currentPanel = _menuPanel;

        AudioManager.Instance.StopPlayingBackgroundSounds();

        _platformSetter.SetRoad();

        _stateArguments._playerMovement.StopMoving();
        _stateArguments._inputManager.DeInitInputHandle();
        _stateArguments._animationHandler.StartStandAnimation();
        _stateArguments._playerLabel.HideLabel();
    }
}
