using System.Collections.Generic;
using UnityEngine;

public class BarrierSpawner : MonoBehaviour
{
    [SerializeField] private DefaultBarrier[] _barrierTypes;
    [SerializeField] private GameObject _barrierPrefab;

    private BarrierIdentifier _barrierIdentifier;
    private Dictionary<GameObject, BarrierKeeper> _barrierOnTheSegment;

    public void InitSpawner()
    {
        _barrierIdentifier = new BarrierIdentifier(_barrierTypes);
        _barrierOnTheSegment = new Dictionary<GameObject, BarrierKeeper>();
    }

    public void SpawnBarrier(GameObject roadSegment, bool isActive)
    {
        GameObject currentBarrier = Instantiate(_barrierPrefab, roadSegment.transform, true);
        BarrierKeeper barrierKeeper = currentBarrier.GetComponent<BarrierKeeper>();
        _barrierOnTheSegment.Add(roadSegment, barrierKeeper);
        SetBarrierOnSegment(roadSegment, isActive);

    }

    public void SetBarrierOnSegment(GameObject roadSegment, bool isActive)
    {
        _barrierOnTheSegment[roadSegment].Init(
            _barrierTypes[Random.Range(0, _barrierTypes.Length)], 
            roadSegment.GetComponent<RoadSegmentKeeper>());
        _barrierOnTheSegment[roadSegment].gameObject.SetActive(isActive);
    }
}
