using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static float PlayerSpeed { get; set; }
    [SerializeField] private float _playerSpeed = 10f;
    [SerializeField] private float _maxPlayerSpeed = 100;

    private Vector3 _targetPosition = new Vector3(0, 0, 0);
    private void Awake()
    {
        PlayerSpeed = _playerSpeed;
    }

    private IEnumerator MovingBall(int currentLine)
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
        if(PlayerSpeed < 0 || PlayerSpeed + changeValue > _maxPlayerSpeed)
            return;

        PlayerSpeed += changeValue;
    }

    private void OnEnable()
    {
        LineSwitcher.OnPlayerMoving += MovingBall;
    }

    private void OnDisable()
    {
        LineSwitcher.OnPlayerMoving -= MovingBall;
    }
}
