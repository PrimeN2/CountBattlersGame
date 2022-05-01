using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform _playerTranform;
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _triggerPrefab;
    [SerializeField]
    [Range(1, 200)] private int _maxCountOfEnemiesInAGroup = 5;
    [SerializeField] private int _triggerDistance = 2;

    private Queue<GameObject> _navMeshAgentsPull;

    private void Awake()
    {
        _navMeshAgentsPull = new Queue<GameObject>();

        StartCoroutine(FillingPool());
    }

    public void Spawn(RoadSegmentKeeper roadSegment)
    {
        for (int i = 0; i < _maxCountOfEnemiesInAGroup; ++i)
        {
            GameObject currentAgent = _navMeshAgentsPull.Dequeue();
            currentAgent.transform.position = roadSegment.GetPlatformCentre() + GetOffsetFor(i);
            currentAgent.gameObject.SetActive(true);
        }
    }

    public void Spawn(Vector3 point)
    {
        for (int i = 0; i < _maxCountOfEnemiesInAGroup; ++i)
        {
            GameObject currentAgent = _navMeshAgentsPull.Dequeue();
            currentAgent.transform.position = point + GetOffsetFor(i);
            currentAgent.gameObject.SetActive(true);
        }
    }

    private IEnumerator FillingPool()
    {
        for (int i = 0; i < 3; ++i)
        {
            FillPool(5);
            yield return new WaitForEndOfFrame();
        }

    }

    private void FillPool(int countOfGroups)
    {
        for (int j = 0; j < _maxCountOfEnemiesInAGroup * countOfGroups; ++j)
        {
            GameObject current = Instantiate(_enemyPrefab, transform);
            current.SetActive(false);

            _navMeshAgentsPull.Enqueue(current);
        }

    }

    private GameObject CreateTrigger()
    {
        return Instantiate(_triggerPrefab, transform);
    }
    private Vector3 GetOffsetFor(int i)
    {
        return new Vector3(Random.Range(-1, 2) * 0.01f * i, 0, 4 + Random.Range(-1, 2) * 0.01f * i);
    }
}

public class Bunch
{
    public delegate GameObject CreateTrigger(Vector3 position);

    private GameObject _trigger;
    private List<NavMeshAgent> _navMeshAgents = new List<NavMeshAgent>();
    private Vector3 _startPosition;

    public Bunch(List<NavMeshAgent> navMeshAgents, Vector3 startPosition, CreateTrigger createTrigger)
    {
        _navMeshAgents = navMeshAgents;
        _startPosition = startPosition;

        for (int i = 0; i < 10; ++i)
        {
            _navMeshAgents[i].gameObject.transform.position = _startPosition + GetOffsetFor(i);
        }
        _trigger = createTrigger(_startPosition + new Vector3(0, 0, 5));
    }

    private Vector3 GetOffsetFor(int i)
    {
        return new Vector3(Random.Range(-1, 2) * 0.01f * i, 0, 4 + Random.Range(-1, 2) * 0.01f * i);
    }
}
