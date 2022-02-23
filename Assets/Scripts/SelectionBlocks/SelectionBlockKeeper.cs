using UnityEngine;

public class SelectionBlockKeeper : MonoBehaviour
{
    public void Set(RoadSegmentKeeper roadSegmentKeeper)
    {
        gameObject.transform.position = roadSegmentKeeper.GetPlatformCentre();
    }
}
