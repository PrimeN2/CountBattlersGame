using UnityEngine;
using TMPro;

public class SelectionBlockKeeper : MonoBehaviour
{
    [SerializeField] private Renderer _leftArea;
    [SerializeField] private Renderer _rightArea;
    [SerializeField] private TextMeshPro _leftLabel;
    [SerializeField] private TextMeshPro _rightLable;

    private Vector3 _selectionBlockOffset = new Vector3(0, 0, -0.075f);

    public void Set(RoadSegmentKeeper roadSegmentKeeper, int rightSum, int leftSum)
    {
        gameObject.transform.position = roadSegmentKeeper.GetPlatformStart() + _selectionBlockOffset;
        _leftLabel.text = $"+{leftSum}";
        _rightLable.text = $"+{rightSum}";
    }
}
