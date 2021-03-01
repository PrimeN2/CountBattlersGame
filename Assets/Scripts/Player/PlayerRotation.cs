using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    [Range(0, 5f)]
    [SerializeField] private float speed = 1;

    private void Update()
    {
        transform.Rotate(new Vector3(speed, 0, 0));
    }
}
