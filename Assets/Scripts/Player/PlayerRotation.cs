using System.Collections;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;

    [SerializeField] private float _rotationSpeed = 10;
    private float _targetPosition = 0;

    private Vector3 _rotationDirection = Vector3.right;

    private IEnumerator RotateToTheSide(int currentLine, int direction)
    {
        _targetPosition = 0.83f * currentLine;
        float startPosition = transform.position.x;
        float distance = Mathf.Abs(startPosition - _targetPosition);

        while (transform.position.x != _targetPosition)
        {
            if (Mathf.Abs(startPosition - transform.position.x) < distance / 2)
            {
                _rotationDirection.z -= 10 * direction * Time.deltaTime;
            }
            else
            {
                _rotationDirection.z += 10 * direction * Time.deltaTime;
            }
            yield return null;
        }

        _rotationDirection.z = 0;
    }

    private void Awake()
    {
        _rotationDirection = new Vector3(1, 0, 0);
    }

    private void Update()
    {
        transform.Rotate(_rotationDirection * _rotationSpeed * _playerMovement.PlayerSpeed * Time.deltaTime, Space.World);
    }
}
