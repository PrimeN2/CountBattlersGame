using System.Collections.Generic;
using UnityEngine;
using System;

public class RoadSegmentKeeper : MonoBehaviour
{
    public static Action<GameObject> OnSegmentOverFliew;
    public List<PlatformKeeper> PlatformKeepers;

    [SerializeField] private PlayerMovement _playerMovement;

    public void Init(Transform parent, PlayerMovement playerMovement)
    {
        transform.SetParent(parent);
        _playerMovement = playerMovement;
    }

    private void Update()
    {
        transform.position -= Vector3.forward * _playerMovement.PlayerSpeed * Time.deltaTime;
    }
    private void LateUpdate()
    {
        if (transform.position.z < -7)
        {
            OnSegmentOverFliew?.Invoke(gameObject);
        }
    }
}
