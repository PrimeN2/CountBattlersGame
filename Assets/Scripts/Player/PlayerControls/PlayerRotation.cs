using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    [Range(0, 5f)]
    [SerializeField] private float _rotationSpeed = 1;
    [SerializeField] private Vector3 _rotationDirection = Vector3.right;

    private void Awake()
    {
        _rotationDirection = Vector3.right;
        _rotationSpeed = 1;
        _rotationDirection *= _rotationSpeed;
    }

    private void Update()
    {
        transform.Rotate(_rotationDirection);
    } 
}
