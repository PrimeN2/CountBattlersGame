using System.Collections;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 10;

    private Vector3 _rotationDirection = Vector3.right;
    private float _targetPosition = 0;

    private IEnumerator RotateToTheSide(int currentLine, int direction)
    {
        _targetPosition = 0.83f * currentLine;
        float startingPosition = transform.position.x;
        float distance = Mathf.Abs(startingPosition - _targetPosition);

        while (transform.position.x != _targetPosition)
        {
            if (Mathf.Abs(startingPosition - transform.position.x) < distance / 2)
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
        transform.Rotate(_rotationDirection * _rotationSpeed * PlayerMovement.PlayerSpeed * Time.deltaTime, Space.World);
    }

    private void OnEnable()
    {
        LineSwitcher.OnPlayerTurning += RotateToTheSide;
    }

    private void OnDisable()
    {
        LineSwitcher.OnPlayerTurning -= RotateToTheSide;
    }
}
