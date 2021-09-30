using System.Collections.Generic;
using UnityEngine;

public class BarrierSpawner : MonoBehaviour
{
    [SerializeField] private DefaultBarrier[] _barrierTypes;
    [SerializeField] private GameObject _barrierPrefab;

    private BarrierIdentifier _barrierIdentifier;
    private Dictionary<GameObject, BarrierKeeper> _barrierOnTheSegment = new Dictionary<GameObject, BarrierKeeper>();

    private void Awake()
    {
        _barrierIdentifier = new BarrierIdentifier(_barrierTypes);
    }

    public void SpawnBarrier(GameObject roadSegment, bool isActive)
    {
        GameObject currentBarrier = Instantiate(_barrierPrefab, Vector3.zero, Quaternion.identity, roadSegment.transform);
        BarrierKeeper barrierKeeper = currentBarrier.GetComponent<BarrierKeeper>();
        _barrierOnTheSegment.Add(roadSegment, barrierKeeper);
        InitBarrierOnSegment(roadSegment, isActive);

    }

    public void InitBarrierOnSegment(GameObject roadSegment, bool isActive)
    {
        _barrierOnTheSegment[roadSegment].Init(
            _barrierTypes[Random.Range(0, _barrierTypes.Length)], 
            roadSegment.GetComponent<RoadSegmentKeeper>());
        _barrierOnTheSegment[roadSegment].gameObject.SetActive(isActive);
    }
}
