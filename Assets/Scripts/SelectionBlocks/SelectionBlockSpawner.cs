using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionBlockSpawner : MonoBehaviour
{
    [SerializeField] private SelectionBlockKeeper _selectionBlock;

    public void SetSelectionBlockOnSegment(RoadSegmentKeeper roadSegmentKeeper, int leftValue, int rightValue)
    {
        SelectionBlockKeeper current = Instantiate(_selectionBlock, transform);
        current.Set(roadSegmentKeeper, leftValue, rightValue);
        current.gameObject.SetActive(true);
    }
}
