using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RoadSegmentSpawner : MonoBehaviour
{
    public Action<RoadSegmentKeeper> OnRoadSegmentAdded;
    public Action OnRoadCompleted;

    [HideInInspector] public float Border;

    [SerializeField] private GameObject _roadSegment;
    [SerializeField] private RoadSegmentKeeper _startRoadSegmentKeeper;
    [SerializeField] private GameObject _startRoadSegmen;
    [SerializeField] private NavMeshSurface _navMeshBuilder;
    [SerializeField] private float _zOffset;

    private List<GameObject> _currentRoadSegments;
    private List<RoadSegmentKeeper> _roadSegmentKeepers;

    private float _segmentLength;

    private void Awake()
    {
        _currentRoadSegments = new List<GameObject>();
        _roadSegmentKeepers = new List<RoadSegmentKeeper>();

        Initialize(_startRoadSegmentKeeper);

        _currentRoadSegments.Add(_startRoadSegmen);
        _roadSegmentKeepers.Add(_startRoadSegmentKeeper);

        Vector3 StartSegmentColliderSize = _startRoadSegmen.GetComponent<Collider>().bounds.size;
        _segmentLength = StartSegmentColliderSize.z;

        Border = StartSegmentColliderSize.x;
    }


    private void Start()
    {
        for (int i = 0; i < 6; ++i)
            SpawnRoadSegment();
        _navMeshBuilder.BuildNavMesh();

        OnRoadCompleted?.Invoke();
    }

    public void SpawnRoadSegment()
    {
        GameObject roadSegment = Instantiate(_roadSegment);
        RoadSegmentKeeper roadSegmentKeeper = roadSegment.GetComponent<RoadSegmentKeeper>();

        Initialize(roadSegmentKeeper);
        PutRoadSegment(roadSegment, roadSegmentKeeper);

        _currentRoadSegments.Add(roadSegment);
        _roadSegmentKeepers.Add(roadSegmentKeeper);

    }

    private void PutRoadSegment(GameObject roadSegment, RoadSegmentKeeper roadSegmentKeeper)
    {
        if (_currentRoadSegments.Count > 0)
            roadSegment.transform.position = 
                _currentRoadSegments[_currentRoadSegments.Count - 1].transform.position + 
                Vector3.forward * _segmentLength;

        OnRoadSegmentAdded?.Invoke(roadSegmentKeeper);
    }

    private void Initialize(RoadSegmentKeeper roadSegmentKeeper)
    {
        roadSegmentKeeper.Init(transform, _zOffset);
    }
}
