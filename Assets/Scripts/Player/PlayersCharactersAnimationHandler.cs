using UnityEngine;

public class PlayersCharactersAnimationHandler : MonoBehaviour
{
    [SerializeField] private PlayerAlliensHandler _playerAlliensHandler;

    private bool _isMoving;
    private bool _isFinished;

    private void Awake()
    {
        _isMoving = false;
        _isFinished = false;
    }

    public void StartFightAnimation()
    {
        SetAnimation();
    }

    public void StartRunAnimation()
    {
        _isMoving = true;
        SetAnimation();
    }

    public void StartWinAnimation()
    {
        _isFinished = true;
        SetAnimation();
    }

    public void StartStandAnimation()
    {
        _isMoving = false;
        SetAnimation();
    }

    private void SetAnimation()
    {
        foreach (var character in _playerAlliensHandler.Characters)
        {
            character.Animator.SetBool("IsMoving", _isMoving);

            if (_isFinished)
                character.Animator.SetTrigger("OnFinished");
        }
    }

    private void OnEnable()
    {
        _playerAlliensHandler.OnCharacterAdded += SetAnimation;
    }

    private void OnDisable()
    {
        _playerAlliensHandler.OnCharacterAdded -= SetAnimation;
    }

}
