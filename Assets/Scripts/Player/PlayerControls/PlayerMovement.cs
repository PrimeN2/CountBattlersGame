using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static float PlayerSpeed { get; private set; }
    [SerializeField] private float _playerSpeed = 10f;
    private void Awake()
    {
        PlayerSpeed = _playerSpeed;
    }
}
