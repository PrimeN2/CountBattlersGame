using System;
using UnityEngine;

public class RoadSegmentKeeper : MonoBehaviour
{
    public Action<GameObject> OnSegmentOverFlew;

    private PlayerMovement _playerMovement;
    private float _bounds;
    private float _zOffset;

    public void Init(Transform parent, PlayerMovement playerMovement, float bounds, float zOffset)
    {
        transform.SetParent(parent);
        _playerMovement = playerMovement;
        _bounds = bounds;
        _zOffset = zOffset;
    }

    public Vector3 GetPlatformCentre()
    {
        return transform.position;
    }

    public Vector3 GetPlatformEnd()
    {
        return transform.position + new Vector3(0, 0, _zOffset);
    }

    private void FixedUpdate()
    {
        if (_playerMovement.PlayerPosition.z - transform.position.z > _bounds)
        {
            OnSegmentOverFlew.Invoke(gameObject);
        }
    }
}
