using UnityEngine;
using TMPro;

public class SelectionBlockKeeper : MonoBehaviour
{
    public bool IsTouched = false;

    private SelectionAreaKeeper[] _selectionAreaKeepers;

    [SerializeField] private Renderer _leftArea;
    [SerializeField] private Renderer _rightArea;
    [SerializeField] private TextMeshPro _leftLabel;
    [SerializeField] private TextMeshPro _rightLable;

    private Vector3 _selectionBlockOffset = new Vector3(0, 0, -0.075f);

    public void Set(RoadSegmentKeeper roadSegmentKeeper)
    {
        int LeftAmount = Random.Range(5, 12);
        int RightAmount = Random.Range(5, 12);

        _selectionAreaKeepers = GetComponentsInChildren<SelectionAreaKeeper>();

        _selectionAreaKeepers[1].Amount = LeftAmount;
        _selectionAreaKeepers[0].Amount = RightAmount;

        gameObject.transform.position = roadSegmentKeeper.GetPlatformStart() + _selectionBlockOffset;
        _leftLabel.text = $"+{LeftAmount}";
        _rightLable.text = $"+{RightAmount}";
    }
}
