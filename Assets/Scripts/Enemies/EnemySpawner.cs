using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    private int _count = 5;

    public void Spawn(RoadSegmentKeeper roadSegment)
    {
        for(int i = 0; i < _count; ++i)
        {
            GameObject current = Instantiate(_enemyPrefab, roadSegment.GetPlatformCentre() + 
                new Vector3(Random.Range(-1, 2) * 0.01f * i, 0, 2 + Random.Range(-1, 2) * 0.01f * i), Quaternion.identity, transform);
        }
    }
}
