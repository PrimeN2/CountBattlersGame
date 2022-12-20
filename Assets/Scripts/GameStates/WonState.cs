using UnityEngine;

public class WonState : BaseGameState
{
    private GameObject _menuPanel;
    private AudioClip _winSound;

    public WonState(IGameStateSwitcher stateSwitcher, GameObject menuPanel, AudioClip winSound, StateArguments stateArguments)
        : base(stateSwitcher, stateArguments)
    {
        _menuPanel = menuPanel;
        _winSound = winSound;
    }

    public override void Load()
    {
        if (_currentPanel != null && _currentPanel != _menuPanel)
            _currentPanel.SetActive(false);
        _menuPanel.SetActive(true);
        _currentPanel = _menuPanel;

        _stateArguments._playerMovement.StopMoving();
        _stateArguments._inputManager.DeinitInputHandle(false);
        _stateArguments._animationHandler.StartWinAnimation();
        _stateArguments._playerLabel.HideLabel();

        SceneController.Instance.StartDelayedReloading();
        AudioManager.Instance.StopPlayingBackgroundSounds();
        AudioManager.Instance.PlaySound(_winSound);
    }
}
