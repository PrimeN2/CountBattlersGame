using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public static float PlayerSpeed { get; set; }
    [SerializeField] private float _playerSpeed = 10f;
    [SerializeField] private Rigidbody _playerRigidbody;
    private void Awake()
    {
        PlayerSpeed = _playerSpeed;
        _playerRigidbody = GetComponent<Rigidbody>();
    }

    private IEnumerator MovingSnowball(int direction)
    {
        for (int i = 0; i < 10; ++i)
        {
            _playerRigidbody.AddForce(Vector3.right * _playerSpeed * -direction);
            yield return null;
        }
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
