using System;
using UnityEngine;

public class RoadSegmentKeeper : MonoBehaviour
{
    private float _zOffset;

    public void Init(float zOffset)
    {
        _zOffset = zOffset;
    }

    public Vector3 GetPlatformCentre()
    {
        return transform.position;
    }

    public Vector3 GetPlatformStart()
    {
        return transform.position + new Vector3(0, 0, _zOffset);
    }
}
