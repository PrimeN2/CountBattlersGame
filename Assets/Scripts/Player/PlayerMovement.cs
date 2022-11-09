using System.Collections;
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

    public Vector3 PlayerPosition { get => _playerTransform.position; }

    private Transform _playerTransform;

    [SerializeField] private RoadSegmentSpawner _roadSegmentSpawner;
    [SerializeField] private PlayerAlliensHandler _playerAlliensHandler;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _playerControlMultiplier;

    private float _playerSpeed = 10f;
    private bool _isStoped = false;
    private Vector3 _direction;

    private Vector3 _deltaDirection;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _playerAlliensHandler = GetComponent<PlayerAlliensHandler>();

        _playerTransform = transform;
        _isStoped = false;
        _playerSpeed = 10f;
        _direction = Vector3.forward;
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

        if (positionX + _playerAlliensHandler.DistanceToFarthestLeft < -_roadSegmentSpawner.Border / 2 ||
            positionX + _playerAlliensHandler.DistanceToFarthestRight > _roadSegmentSpawner.Border / 2)
            return false;
        _deltaDirection = Vector3.right * xOffset;
        return true;
    }

    public IEnumerator MoveTo(Vector3 position)
    {
        while (transform.position != position)
        {
            transform.position = Vector3.MoveTowards(transform.position, position, 2 * Time.deltaTime);

            yield return new WaitForEndOfFrame();
        }
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
