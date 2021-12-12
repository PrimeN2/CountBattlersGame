using System.Collections;
using UnityEngine;

[RequireComponent(typeof(LineSwitcher))]
public class PlayerMovement : MonoBehaviour
{
    public float PlayerSpeed {
        get
        {
            if (_isStoped) return 0;
            else return _playerSpeed;
        }
    }
    [SerializeField] private float _playerSpeed = 10f;

    [SerializeField] private float _maxPlayerSpeed = 100;

    [SerializeField] private LineSwitcher _lineSwitcher;

    private Vector3 _targetPosition = new Vector3(0, 0, 0);
    private bool _isStoped = false;

    private void Awake()
    {
        _lineSwitcher = GetComponent<LineSwitcher>();
        _isStoped = false;
        _playerSpeed = 10f;
    }

    private IEnumerator MoveBall(int currentLine)
    {
        _targetPosition = new Vector3(0.83f * currentLine, transform.position.y, transform.position.z);

        while (transform.position.x != _targetPosition.x)
        {
            _targetPosition = new Vector3(0.83f * currentLine, transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, PlayerSpeed * Time.deltaTime);

            yield return null;
        }
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

    private void OnEnable()
    {
        _lineSwitcher.OnPlayerMoving += MoveBall;
    }

    private void OnDisable()
    {
        _lineSwitcher.OnPlayerMoving -= MoveBall;
    }
}
