using UnityEngine;
using TMPro;

public class SelectionBlockKeeper : MonoBehaviour
{
    public bool IsTouched = false;

    private SelectionAreaKeeper[] _selectionAreaKeepers;

    [SerializeField] private TextMeshPro _leftLabel;
    [SerializeField] private TextMeshPro _rightLable;

    private Vector3 _selectionBlockOffset = new Vector3(0, 0, -0.075f);

    public void Set(RoadSegmentKeeper roadSegmentKeeper, int firstValue, int secondValue)
    {
        _selectionAreaKeepers = GetComponentsInChildren<SelectionAreaKeeper>();

        int rand = Random.Range(0, 2);

        if (rand == 1)
        {
            _selectionAreaKeepers[1].Init(firstValue, false);
            _selectionAreaKeepers[0].Init(secondValue, false);

            _leftLabel.text = $"+{firstValue}";
            _rightLable.text = $"+{secondValue}";
        }
        else
        {
            _selectionAreaKeepers[1].Init(secondValue, false);
            _selectionAreaKeepers[0].Init(firstValue, false);

            _leftLabel.text = $"+{secondValue}";
            _rightLable.text = $"+{firstValue}";
        }
        gameObject.transform.position = roadSegmentKeeper.GetPlatformStart() + _selectionBlockOffset;
    }

    public void SetWithMultiplier(RoadSegmentKeeper roadSegmentKeeper, int firstValue, int multiplier)
    {
        _selectionAreaKeepers = GetComponentsInChildren<SelectionAreaKeeper>();

        int rand = Random.Range(0, 2);

        if (rand == 1)
        {
            _selectionAreaKeepers[1].Init(firstValue, false); //left
            _selectionAreaKeepers[0].Init(multiplier, true); //right

            _leftLabel.text = $"+{firstValue}";
            _rightLable.text = $"x{multiplier}";
        }
        else
        {
            _selectionAreaKeepers[1].Init(multiplier, true);
            _selectionAreaKeepers[0].Init(firstValue, false);

            _leftLabel.text = $"x{multiplier}";
            _rightLable.text = $"+{firstValue}";
        }
        gameObject.transform.position = roadSegmentKeeper.GetPlatformStart() + _selectionBlockOffset;
    }
}
