using System.Collections.Generic;
using UnityEngine;

public class RoadSegmentMover : MonoBehaviour
{
    [SerializeField] private RoadSegmentSpawner _roadSegmentSpawner;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private List<IMovable> _objectsToMove;

    private Vector3 _direction = Vector3.back;

    private void Start()
    {
        _objectsToMove = new List<IMovable>();

        foreach (IMovable roadSegmentKeeper in _roadSegmentSpawner.RoadSegmentKeepers)
        {
            _objectsToMove.Add(roadSegmentKeeper);
        }
    }

    private void Update()
    {
        float speed = _playerMovement.PlayerSpeed * Time.deltaTime;

        foreach (var objectToMove in _objectsToMove)
        {
            objectToMove.Move(_direction, speed);
        }
    }
}
