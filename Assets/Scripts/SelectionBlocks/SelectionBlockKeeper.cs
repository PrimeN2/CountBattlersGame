using UnityEngine;
using TMPro;

public class SelectionBlockKeeper : MonoBehaviour
{
    [SerializeField] private Renderer _leftArea;
    [SerializeField] private Renderer _rightArea;
    [SerializeField] private TextMeshPro _leftLabel;
    [SerializeField] private TextMeshPro _rightLable;

    public void Set(RoadSegmentKeeper roadSegmentKeeper, int rightSum, int leftSum)
    {
        gameObject.transform.position = roadSegmentKeeper.GetPlatformCentre();
        _leftLabel.text = $"+{leftSum}";
        _rightLable.text = $"+{rightSum}";
    }
}
