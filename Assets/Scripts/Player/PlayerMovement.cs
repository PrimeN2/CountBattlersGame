using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static float PlayerSpeed { get; set; }
    [SerializeField] private float _playerSpeed = 10f;

    private Vector3 _targetPosition = new Vector3(0, 0, 0);
    private void Awake()
    {
        PlayerSpeed = _playerSpeed;
    }

    private IEnumerator MovingSnowball(int currentLine)
    {
        _targetPosition = new Vector3(0.83f * currentLine, transform.position.y, transform.position.z);

        while (transform.position.x != _targetPosition.x)
        {
            _targetPosition = new Vector3(0.83f * currentLine, transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, PlayerSpeed * Time.deltaTime);

            yield return null;
        }
    }

    public void ChangeSpeed(float changeValue)
    {
        if(PlayerSpeed < 0)
            return;

        PlayerSpeed -= changeValue;
    }

    private void OnEnable()
    {
        LineController.OnPlayerMoving += MovingSnowball;
    }

    private void OnDisable()
    {
        LineController.OnPlayerMoving -= MovingSnowball;
    }
}
