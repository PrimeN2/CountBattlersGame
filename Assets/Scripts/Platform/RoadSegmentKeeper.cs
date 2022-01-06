using System.Collections.Generic;
using UnityEngine;
using System;

public class RoadSegmentKeeper : MonoBehaviour
{
    public void Init(Transform parent)
    {
        transform.SetParent(parent);
    }

    public Vector3 GetPointToSpawn(Lines.Line line, Vector3 offset)
    {
        Vector3 position = new Vector3(offset.x * (int)line, offset.y, offset.z) + transform.position;
        return position;
    }
}
