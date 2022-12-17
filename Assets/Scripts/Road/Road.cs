using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RoadSegmentKeeper))]
public class Road : MonoBehaviour
{
    [HideInInspector] public List<RoadSegmentKeeper> RoadSegments;
    [HideInInspector] public float Border;

    [SerializeField] private GameObject _road;
    [SerializeField] private float _zOffset;

    public void Initialize()
    {
        Border = _road.GetComponentInChildren<Collider>().bounds.size.x;

        foreach(var roadSegment in _road.GetComponentsInChildren<RoadSegmentKeeper>())
        {
            RoadSegments.Add(roadSegment);
            roadSegment.Init(_zOffset);
        }
    }
}
