using System.Collections.Generic;
using UnityEngine;

public class RoadSegmentMover : MonoBehaviour
{
    [SerializeField] private RoadSegmentSpawner _roadSegmentSpawner;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private List<IMovable> _objectsToMove;

    private Vector3 _derection = Vector3.back;

    private void Start()
    {
        _objectsToMove = new List<IMovable>();

        foreach (IMovable roadSegmentKeeper in _roadSegmentSpawner.RoadSegmentKeepers)
        {
            _objectsToMove.Add(roadSegmentKeeper);
        }
    }

    private void FixedUpdate()
    {
        foreach(var objectToMove in _objectsToMove)
        {
            objectToMove.Move(_derection, _playerMovement.PlayerSpeed * Time.deltaTime);
        }
    }
}
