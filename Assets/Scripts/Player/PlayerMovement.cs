using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float PlayerSpeed {
        get
        {
            if (_isStoped) return 0;
            else return _playerSpeed;
        }
    }

    public Vector3 PlayerPosition { get => transform.position; }

    [SerializeField] private RoadSegmentSpawner _roadSegmentSpawner;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _playerControlMultiplier;

    private float _playerSpeed = 10f;
    private bool _isStoped = false;
    private Vector3 _direction;
    private float _safeZone;


    private Vector3 _deltaDirection;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _isStoped = false;
        _playerSpeed = 10f;
        _direction = Vector3.forward;
        _safeZone = 0.4f;
    }

    private void FixedUpdate()
    {
        _rigidbody.MovePosition(_rigidbody.position + _direction * PlayerSpeed * Time.deltaTime + _deltaDirection);
        _deltaDirection = Vector3.zero;
    }

    public bool TryMove(float xOffset)
    {
        xOffset *= _playerControlMultiplier;
        float positionX = _rigidbody.position.x + xOffset;
        
        if (positionX + -_safeZone < _roadSegmentSpawner.LeftBorder / 2 || 
            positionX + _safeZone > _roadSegmentSpawner.RightBorder / 2)
            return false;
        _deltaDirection = Vector3.right * xOffset;
        return true;
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
