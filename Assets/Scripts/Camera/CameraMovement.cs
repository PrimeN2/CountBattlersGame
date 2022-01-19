using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _movementSpeed;

    private void LateUpdate()
    {
        Vector3 newPosition = Vector3.Lerp(transform.position, _target.position + _offset, _movementSpeed * Time.deltaTime);
        transform.position = new Vector3(newPosition.x, transform.position.y, _target.position.z + _offset.z);
    }
}
