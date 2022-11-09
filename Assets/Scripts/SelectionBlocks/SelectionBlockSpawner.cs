using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionBlockSpawner : MonoBehaviour
{
    [SerializeField] private SelectionBlockKeeper _selectionBlock;

    public void SetSelectionBlockOnSegment(RoadSegmentKeeper roadSegmentKeeper, int value, int decreasedValue)
    {
        SelectionBlockKeeper current = Instantiate(_selectionBlock, transform);

        short multiplier = 5;

        if (HasMultiplier(ref multiplier, value))
            SetBlockWithMultiplier(current, roadSegmentKeeper, multiplier, value);
        else
            SetBlock(current, roadSegmentKeeper, value, decreasedValue);

    }

    private void SetBlock(SelectionBlockKeeper current, RoadSegmentKeeper roadSegmentKeeper, int value, int decreasedValue)
    {
        current.Set(roadSegmentKeeper, value, decreasedValue);
        current.gameObject.SetActive(true);
    }

    private void SetBlockWithMultiplier(SelectionBlockKeeper current, RoadSegmentKeeper roadSegmentKeeper, short multiplier, int value)
    {
        current.Set(roadSegmentKeeper, value, multiplier);
        current.gameObject.SetActive(true);
    }

    private bool HasMultiplier(ref short multiplier, int value)
    {
        for (int i = 5; i > 0; i--)
            if (multiplier * value >= 100)
                multiplier--;

        int hasMultiplier = Random.Range(0, 2);

        if (hasMultiplier == 1)
            return true;
        else
            return false;
    }
}
