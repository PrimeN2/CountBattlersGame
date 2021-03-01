using System.Collections.Generic;
using UnityEngine;
using System;

public class PlatformsBlockKeeper : MonoBehaviour
{
    public static Action<GameObject> OnPlatformOverFliew;
    public List<PlatformKeeper> PlatformKeepers;

    public float MovingSpeed { get => _movingSpeed; set => _movingSpeed = value; }
    [SerializeField] private float _movingSpeed = 10f;

    private void Awake()
    {
        _movingSpeed = 10f;
    }

    private void Update()
    {
        transform.position -= Vector3.forward * _movingSpeed * Time.deltaTime;
    }
    private void LateUpdate()
    {
        if (transform.position.z < -7)
        {
            OnPlatformOverFliew?.Invoke(gameObject);
        }
    }
}
