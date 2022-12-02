using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionBlockSpawner : MonoBehaviour
{
    [SerializeField] private SelectionBlockKeeper _selectionBlock;

    public void SetSelectionBlockOnSegment(RoadSegmentKeeper roadSegmentKeeper, int value, int decreasedValue, int multiplier)
    {
        SelectionBlockKeeper current = Instantiate(_selectionBlock, transform);

        if (multiplier <= 0)
            SetBlock(current, roadSegmentKeeper, value, decreasedValue);
        else SetBlockWithMultiplier(current, roadSegmentKeeper, multiplier, value);
    }

    private void SetBlock(SelectionBlockKeeper current, RoadSegmentKeeper roadSegmentKeeper, int value, int decreasedValue)
    {
        current.Set(roadSegmentKeeper, value, decreasedValue);
        current.gameObject.SetActive(true);
    }

    private void SetBlockWithMultiplier(SelectionBlockKeeper current, RoadSegmentKeeper roadSegmentKeeper, int multiplier, int value)
    {
        current.SetWithMultiplier(roadSegmentKeeper, value, multiplier);
        current.gameObject.SetActive(true);
    }
}
