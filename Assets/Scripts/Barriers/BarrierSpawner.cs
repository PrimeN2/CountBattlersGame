using System.Collections.Generic;
using UnityEngine;

public class BarrierSpawner : MonoBehaviour
{
    [SerializeField] private DefaultBarrier[] _barrierTypes;
    [SerializeField] private BarrierKeeper _barrierKeeper;

    private BarrierIdentifier _barrierIdentifier;
    private Queue<BarrierKeeper> _barriersPool;

    private void Awake()
    {
        _barriersPool = new Queue<BarrierKeeper>();
        _barrierIdentifier = new BarrierIdentifier(_barrierTypes);

        Spawn();
    }

    public void SpawnBarrierOnSegment(RoadSegmentKeeper roadSegmentKeeper)
    {
        for (int i = 0; i < 3; ++i)
        {
            BarrierKeeper currentBarrier = _barriersPool.Dequeue();
            currentBarrier.Init(
                _barrierTypes[Random.Range(0, _barrierTypes.Length)], roadSegmentKeeper, i);
            currentBarrier.gameObject.SetActive(true);
            _barriersPool.Enqueue(currentBarrier);
        }
    }

    private void Spawn()
    {
        for(int i = 0; i < 20; ++i)
        {
            BarrierKeeper current = Instantiate(_barrierKeeper, transform);
            current.gameObject.SetActive(false);
            _barriersPool.Enqueue(current);
        }
    }
}
