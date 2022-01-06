using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float PlayerSpeed {
        get
        {
            if (_isStoped) return 0;
            else return _playerSpeed;
        }
    }

    [SerializeField] private float _maxPlayerSpeed = 100;
    [SerializeField] private float _playerControlMultiplier;

    private float _playerSpeed = 10f;
    private bool _isStoped = false;

    private void Awake()
    {
        _isStoped = false;
        _playerSpeed = 10f;
    }

    public void TryMove(float xOffset)
    {
        xOffset *= _playerControlMultiplier;
        float positionX = transform.position.x + xOffset;
        transform.position += positionX > -1 && positionX < 1 ? new Vector3(xOffset, 0, 0) : Vector3.zero;
    }

    public void TryChangeSpeed(float changeValue)
    {
        if(PlayerSpeed < 0 || PlayerSpeed + changeValue > _maxPlayerSpeed || _isStoped)
                return;

        _playerSpeed += changeValue;
    }

    public void StopMoving()
    {
        _isStoped = true;
    }

    public void ContinueMoving()
    {
        if (!_isStoped)
            return;
        _isStoped = false;

    }
}
