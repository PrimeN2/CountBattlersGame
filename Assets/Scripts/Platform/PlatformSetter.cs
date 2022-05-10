using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlatformSetter : MonoBehaviour
{
    [Header("Spawners")]
    [SerializeField] private RoadSegmentSpawner _roadSegmentSpawner;
    [SerializeField] private SelectionBlockSpawner _selectionBlockSpawner;
    [SerializeField] private CharacterSpawner _characterSpawner;
    [SerializeField] private BarrierSpawner _barrierSpawner;

    private int[] _segmentsOrder;

    private void DefineObjects(RoadSegmentKeeper roadSegmentKeeper)
    {
        _selectionBlockSpawner.SetSelectionBlockOnSegment(roadSegmentKeeper);
        _barrierSpawner.SpawnBarrierOnSegment(roadSegmentKeeper);
        _characterSpawner.Spawn(roadSegmentKeeper);
    }

    private void OnEnable()
    {
        _roadSegmentSpawner.OnRoadSegmentAdded += DefineObjects;
    }

    private void OnDisable()
    {
        _roadSegmentSpawner.OnRoadSegmentAdded -= DefineObjects;
    }
}

