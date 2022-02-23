using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionBlockSpawner : MonoBehaviour
{
    [SerializeField] private SelectionBlockKeeper _selectionBlock;

    private Dictionary<RoadSegmentKeeper, SelectionBlockKeeper[]> _selectionBlocksOnSegment;

    public void InitSpawner()
    {
        _selectionBlocksOnSegment = new Dictionary<RoadSegmentKeeper, SelectionBlockKeeper[]>();
    }

    public void SpawnNewSelectionBlock(RoadSegmentKeeper roadSegmentKeeper, int count, bool isActive)
    {
        SelectionBlockKeeper[] currentKeepers = new SelectionBlockKeeper[count];

        for (int i = 0; i < count; ++i)
        {
            SelectionBlockKeeper currentKeeper = Instantiate(_selectionBlock, roadSegmentKeeper.transform, true);
            currentKeepers[i] = currentKeeper;
        }
        _selectionBlocksOnSegment.Add(roadSegmentKeeper, currentKeepers);
        SetSelectionBlockOnSegment(roadSegmentKeeper, isActive);
    }
    public void SetSelectionBlockOnSegment(RoadSegmentKeeper roadSegmentKeeper, bool isActive)
    {
        foreach (var selectionBlockKeeper in _selectionBlocksOnSegment[roadSegmentKeeper])
        {
            selectionBlockKeeper.Set(roadSegmentKeeper);
            selectionBlockKeeper.gameObject.SetActive(isActive);
        }
    }
}
