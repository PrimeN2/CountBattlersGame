using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RoadSegmentSpawner : MonoBehaviour
{
    [HideInInspector] public float LeftBorder;
    [HideInInspector] public float RightBorder;

    [SerializeField] private SelectionBlockSpawner _selectionBlockSpawner;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private GameObject _roadSegment;
    [SerializeField] private RoadSegmentKeeper _startRoadSegmentKeeper;
    [SerializeField] private GameObject _startRoadSegmen;
    [SerializeField] private BarrierSpawner _barrierSpawner;
    [SerializeField] private int _segmentsCount;
    [SerializeField] private float _bounds;

    private List<GameObject> _currentRoadSegments;
    private List<RoadSegmentKeeper> _roadSegmentKeepers;

    private NavMeshSurface _navMeshSurface;
    private float _segmentLength;



    private void Awake()
    {
        _selectionBlockSpawner.InitSpawner();
        _barrierSpawner.InitSpawner();
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

        for (int i = 0; i < _segmentsCount; ++i)
        {
            SpawnRoadSegment();
        }
        _enemySpawner.Spawn(_startRoadSegmentKeeper);
        _navMeshSurface.BuildNavMesh();
    }

    private void SpawnRoadSegment()
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

        _selectionBlockSpawner.SetSelectionBlockOnSegment(roadSegmentKeeper, true);
        _barrierSpawner.SetBarrierOnSegment(roadSegmentKeeper, true, 4);
        _navMeshSurface.BuildNavMesh();
    }

    private void Initialize(RoadSegmentKeeper roadSegmentKeeper)
    {
        roadSegmentKeeper.Init(transform, _playerMovement, _bounds);
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
