using UnityEngine;

public class SelectionAreaKeeper : MonoBehaviour
{
    private SelectionBlockKeeper _selectionBlockKeeper;

    private void OnEnable()
    {
        _selectionBlockKeeper = GetComponentInParent<SelectionBlockKeeper>();
    }

    public SelectionBlockKeeper GetBlock()
    {
        return _selectionBlockKeeper;
    }
}
