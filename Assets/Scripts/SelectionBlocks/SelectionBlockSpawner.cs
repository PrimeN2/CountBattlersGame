using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionBlockSpawner : MonoBehaviour
{
    [SerializeField] private SelectionBlockKeeper _selectionBlock;

    private Queue<SelectionBlockKeeper> _selectionBlocksPull;

    public void InitSpawner()
    {
        _selectionBlocksPull = new Queue<SelectionBlockKeeper>();

        Spawn();
    }

    public void SetSelectionBlockOnSegment(RoadSegmentKeeper roadSegmentKeeper, bool isActive)
    {
        SelectionBlockKeeper currentSelectionBlock = _selectionBlocksPull.Dequeue();
        currentSelectionBlock.Set(roadSegmentKeeper, Random.Range(1, 51), Random.Range(1, 51));
        currentSelectionBlock.gameObject.SetActive(isActive);
        _selectionBlocksPull.Enqueue(currentSelectionBlock);
    }

    private void Spawn()
    {
        for (int i = 0; i < 5; ++i)
        {
            SelectionBlockKeeper current = Instantiate(_selectionBlock, transform);
            current.gameObject.SetActive(false);
            _selectionBlocksPull.Enqueue(current);
        }
    }
}
