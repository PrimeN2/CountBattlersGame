using UnityEngine;

public class SelectionAreaKeeper : MonoBehaviour
{
    public int Amount { get; private set; }
    public bool IsMultiplyable { get; private set; }

    private SelectionBlockKeeper _selectionBlockKeeper;

    private void OnEnable()
    {
        _selectionBlockKeeper = GetComponentInParent<SelectionBlockKeeper>();
    }

    public void Init(int amount, bool isMultiplyable)
    {
        Amount = amount;
        IsMultiplyable = isMultiplyable;
    }

    public SelectionBlockKeeper GetBlock()
    {
        return _selectionBlockKeeper;
    }
}
