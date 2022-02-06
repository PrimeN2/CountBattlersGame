using System;
using UnityEngine;

public class RoadSegmentKeeper : MonoBehaviour
{
    public Action<GameObject> OnSegmentOverFlew;

    private PlayerMovement _playerMovement;
    private float _bounds;

    public void Init(Transform parent, PlayerMovement playerMovement, float bounds)
    {
        transform.SetParent(parent);
        _playerMovement = playerMovement;
        _bounds = bounds;
    }

    public Vector3 GetPointToSpawn(Vector3 position)
    {
        return position + transform.position;
    }

    public void Unsubscribe(Action<GameObject> action)
    {
        OnSegmentOverFlew -= action;
    }

    private void Update()
    {
        if (_playerMovement.PlayerPosition.z - transform.position.z > _bounds)
        {
            OnSegmentOverFlew.Invoke(gameObject);
        }
    }
}
