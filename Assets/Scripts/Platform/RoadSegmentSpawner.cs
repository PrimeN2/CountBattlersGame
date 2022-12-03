using System;
using UnityEngine;

public class RoadSegmentSpawner : MonoBehaviour
{
    public event Action<RoadSegmentKeeper> OnRoadSegmentAdded;
    public event Action OnRoadCompleted;

    [HideInInspector] public float Border;

    [SerializeField] private GameObject _road;
    [SerializeField] private float _zOffset;

    public void Initialize()
    {
        Border = _road.GetComponentInChildren<Collider>().bounds.size.x;

        foreach(var roadSegment in _road.GetComponentsInChildren<RoadSegmentKeeper>())
            InitRoadSegment(roadSegment);

        OnRoadCompleted?.Invoke();
    }

    private void InitRoadSegment(RoadSegmentKeeper roadSegment)
    {
        roadSegment.Init(_zOffset);
        OnRoadSegmentAdded?.Invoke(roadSegment);
    }
}
