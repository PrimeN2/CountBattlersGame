using System.Collections.Generic;
using UnityEngine;

public class BarrierSpawner : MonoBehaviour
{
    [SerializeField] private DefaultBarrier[] _barrierTypes;
    [SerializeField] private GameObject _barrierPrefab;

    private Quaternion _rotation;
    private Vector3 _position;
    private List<GameObject> _barriersPool;

    private void Start()
    {
        _barriersPool = new List<GameObject>();
    }

    public void SpawnBarrier(GameObject roadSegment, bool isActive)
    {
        roadSegment.GetComponent<RoadSegment>().GetPointToSpawn(_barrierTypes[0], out _position, out _rotation);
        GameObject currentBarrier = Instantiate(_barrierPrefab, _position, _rotation, roadSegment.transform);
        currentBarrier.SetActive(isActive);
    }
}
