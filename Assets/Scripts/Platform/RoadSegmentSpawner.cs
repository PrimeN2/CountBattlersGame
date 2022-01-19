using System.Collections.Generic;
using UnityEngine;

public class RoadSegmentSpawner : MonoBehaviour
{
    public float LeftBorder;
    public float RightBorder;

    [SerializeField] private GameObject _roadSegmentPrefab;
    [SerializeField] private GameObject _startRoadSegment;
    //[SerializeField] private BarrierSpawner _barrierSpawner;
    [SerializeField] private int _segmentsCount;

    private List<GameObject> _currentRoadSegments;
    private List<RoadSegmentKeeper> _roadSegmentKeepers;
    private float _segmentLength;

    private void Awake()
    {
        _currentRoadSegments = new List<GameObject>();
        _roadSegmentKeepers = new List<RoadSegmentKeeper>();

        _currentRoadSegments.Add(_startRoadSegment);
        _roadSegmentKeepers.Add(_startRoadSegment.GetComponent<RoadSegmentKeeper>());

        _segmentLength = _startRoadSegment.GetComponent<Collider>().bounds.size.z;
        LeftBorder = -_startRoadSegment.GetComponent<Collider>().bounds.size.x;
        RightBorder = _startRoadSegment.GetComponent<Collider>().bounds.size.x;

        //_barrierSpawner.SpawnBarrier(_currentRoadSegments[0], false);
        for (int i = 0; i < _segmentsCount; ++i)
        {
            SpawnRoadSegment();

            //if (i <= 3)
            //    _barrierSpawner.SpawnBarrier(_currentRoadSegments[i + 1], false);
            //else
            //    _barrierSpawner.SpawnBarrier(_currentRoadSegments[i + 1], true);
        }
    }

    private void SpawnRoadSegment()
    {
        //It is better not to change the order
        GameObject roadSegment = Instantiate(_roadSegmentPrefab);
        RoadSegmentKeeper roadSegmentKeeper = roadSegment.GetComponent<RoadSegmentKeeper>();
        roadSegmentKeeper.Init(transform);
        PutRoadSegment(roadSegment);
        _currentRoadSegments.Add(roadSegment);
        _roadSegmentKeepers.Add(roadSegmentKeeper);
    }
    private void ReuseRoadSegment(GameObject roadSegment)
    {
        //_barrierSpawner.InitBarrierOnSegment(roadSegment, true);
        PutRoadSegment(roadSegment);
        _currentRoadSegments.Remove(roadSegment);
        _currentRoadSegments.Add(roadSegment);
    }

    private void PutRoadSegment(GameObject roadSegment)
    {
        if (_currentRoadSegments.Count > 0)
            roadSegment.transform.position = 
                _currentRoadSegments[_currentRoadSegments.Count - 1].transform.position + 
                Vector3.forward * _segmentLength;
    }
}
