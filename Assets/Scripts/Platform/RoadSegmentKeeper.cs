using System.Collections.Generic;
using UnityEngine;
using System;

public class RoadSegmentKeeper : MonoBehaviour, IMovable
{
    public static Action<GameObject> OnSegmentOverFliew;
    public List<PlatformKeeper> PlatformKeepers;

    public void Init(Transform parent)
    {
        transform.SetParent(parent);
    }

    public Vector3 GetPointToSpawn(LineSwitcher.Line line, Vector3 offset)
    {
        Vector3 position = new Vector3(offset.x * (int)line, offset.y, offset.z) + transform.position;
        return position;
    }

    public void Move(Vector3 direction, float speed)
    {
        transform.Translate(direction * speed);
    }

    private void LateUpdate()
    {
        if (transform.position.z < -7)
        {
            OnSegmentOverFliew?.Invoke(gameObject);
        }
    }
}
