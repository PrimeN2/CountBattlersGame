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
#if UNITY_EDITOR
        _menuPanel.SetActive(true);
#endif
        _currentPanel = _menuPanel;

        AudioManager.Instance.StartPlayingBackgroundSounds();

        _stateArguments._playerMovement.ContinueMoving();
        _stateArguments._inputManager.InitInputHandle();
        _stateArguments._animationHandler.StartRunAnimation();
        _stateArguments._playerLabel.ShowLabel();
    }
}
