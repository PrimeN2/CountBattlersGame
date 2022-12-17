using UnityEngine;

public class BarrierKeeper : MonoBehaviour
{
    [SerializeField] private Vector3 _position;

    private const float DISTANCE_MULTIPLIER = 6;

    public void SetPositionOn(RoadSegmentKeeper roadSegment, int current)
    {
        transform.position = roadSegment.GetPlatformStart() + 
            new Vector3(_position.x * Random.Range(-1, 2), _position.y,
            _position.z + (current + 1) * DISTANCE_MULTIPLIER);
    }
}
