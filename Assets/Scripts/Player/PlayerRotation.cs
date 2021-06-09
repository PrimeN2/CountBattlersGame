using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerRotation : MonoBehaviour
{
    public static Vector3 _rotationDirection = Vector3.right;

    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _rotationSpeed = 50;

    private IEnumerable RotateToTheSide(int direction)
    {
        _rotationDirection = new Vector3(1, rotation * direction, 0); 

        yield return new WaitForFixedUpdate();
    }

    private void Awake()
    {
        _rotationDirection = Vector3.right;
        _rotationSpeed = 50;
        _rigidbody = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        //transform.rotation = Quaternion.AngleAxis(Mathf.Repeat(_rotationSpeed, 360), _rotationDirection);
        //_rotationSpeed += 4;
        _rigidbody.AddRelativeTorque(_rotationDirection * _rotationSpeed);
    }

    //private void OnEnable()
    //{
    //    LineController.OnPlayerMoving += RotateToTheSide;
    //}

    //private void OnDisable()
    //{
    //    LineController.OnPlayerMoving -= RotateToTheSide;
    //}
}
