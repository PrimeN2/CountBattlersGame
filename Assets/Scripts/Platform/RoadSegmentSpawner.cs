using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RoadSegmentSpawner : MonoBehaviour
{
    public Action<RoadSegmentKeeper> OnRoadSegmentAdded;

    [HideInInspector] public float LeftBorder;
    [HideInInspector] public float RightBorder;

    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private GameObject _roadSegment;
    [SerializeField] private RoadSegmentKeeper _startRoadSegmentKeeper;
    [SerializeField] private GameObject _startRoadSegmen;
    [SerializeField] private int _segmentsCount;
    [SerializeField] private float _zOffset;
    [SerializeField] private float _bounds;

    private List<GameObject> _currentRoadSegments;
    private List<RoadSegmentKeeper> _roadSegmentKeepers;

    private NavMeshSurface _navMeshSurface;
    private float _segmentLength;

    private void Awake()
    {
        _currentRoadSegments = new List<GameObject>();
        _roadSegmentKeepers = new List<RoadSegmentKeeper>();

        Initialize(_startRoadSegmentKeeper);
        _navMeshSurface = _startRoadSegmen.GetComponent<NavMeshSurface>();

        _currentRoadSegments.Add(_startRoadSegmen);
        _roadSegmentKeepers.Add(_startRoadSegmentKeeper);

        Vector3 StartSegmentColliderSize = _startRoadSegmen.GetComponent<Collider>().bounds.size;
        _segmentLength = StartSegmentColliderSize.z;
        LeftBorder = -StartSegmentColliderSize.x;
        RightBorder = StartSegmentColliderSize.x;
    }

    private void Start()
    {
        for (int i = 0; i < _segmentsCount; ++i)
            SpawnRoadSegment();
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
    private void ReuseRoadSegment(GameObject roadSegment)
    {
        RoadSegmentKeeper roadSegmentKeeper = roadSegment.GetComponent<RoadSegmentKeeper>();

        PutRoadSegment(roadSegment, roadSegmentKeeper);

        _currentRoadSegments.Remove(roadSegment);
        _currentRoadSegments.Add(roadSegment);
    }

    private void PutRoadSegment(GameObject roadSegment, RoadSegmentKeeper roadSegmentKeeper)
    {
        if (_currentRoadSegments.Count > 0)
            roadSegment.transform.position = 
                _currentRoadSegments[_currentRoadSegments.Count - 1].transform.position + 
                Vector3.forward * _segmentLength;

        _navMeshSurface.BuildNavMesh();
        OnRoadSegmentAdded?.Invoke(roadSegmentKeeper);
    }

    private void Initialize(RoadSegmentKeeper roadSegmentKeeper)
    {
        roadSegmentKeeper.Init(transform, _playerMovement, _bounds, _zOffset);
        roadSegmentKeeper.OnSegmentOverFlew += ReuseRoadSegment;
    }

    private void OnDisable()
    {
        foreach(var roadSegmentKeeper in _roadSegmentKeepers)
        {
            roadSegmentKeeper.OnSegmentOverFlew -= ReuseRoadSegment;
        }
    }
}
