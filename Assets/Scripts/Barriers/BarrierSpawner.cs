using System.Collections.Generic;
using UnityEngine;

public class BarrierSpawner : MonoBehaviour
{
    [SerializeField] private BarrierKeeper[] _barriers;

    private Queue<BarrierKeeper> _barriersPool;

    public void Init()
    {
        _barriersPool = new Queue<BarrierKeeper>();

        SetupPool();
    }

    public void SpawnBarrierOnSegment(RoadSegmentKeeper roadSegmentKeeper)
    {
        for (int i = 0; i < 3; ++i)
        {
            BarrierKeeper currentBarrier = _barriersPool.Dequeue();

            currentBarrier.SetPositionOn(roadSegmentKeeper, i);
            currentBarrier.gameObject.SetActive(true);

            _barriersPool.Enqueue(currentBarrier);
        }
    }

    private void SetupPool()
    {
        for(int i = 0; i < 20; ++i)
        {
            BarrierKeeper current = Instantiate(_barriers[Random.Range(0, _barriers.Length)], transform);
            current.gameObject.SetActive(false);
            _barriersPool.Enqueue(current);
        }
    }
}
