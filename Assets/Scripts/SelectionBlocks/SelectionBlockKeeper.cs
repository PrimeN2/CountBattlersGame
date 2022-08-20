using UnityEngine;
using TMPro;

public class SelectionBlockKeeper : MonoBehaviour
{
    public bool IsTouched = false;

    private SelectionAreaKeeper[] _selectionAreaKeepers;

    [SerializeField] private TextMeshPro _leftLabel;
    [SerializeField] private TextMeshPro _rightLable;

    private Vector3 _selectionBlockOffset = new Vector3(0, 0, -0.075f);

    public void Set(RoadSegmentKeeper roadSegmentKeeper, int leftValue, int rightValue)
    {
        _selectionAreaKeepers = GetComponentsInChildren<SelectionAreaKeeper>();

        _selectionAreaKeepers[1].Amount = leftValue;
        _selectionAreaKeepers[0].Amount = rightValue;

        gameObject.transform.position = roadSegmentKeeper.GetPlatformStart() + _selectionBlockOffset;
        _leftLabel.text = $"+{leftValue}";
        _rightLable.text = $"+{rightValue}";
    }
}
