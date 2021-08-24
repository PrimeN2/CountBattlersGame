using UnityEngine;

public class RoadSegment : MonoBehaviour
{
    private Vector3 _offset;
    private float _lineWidth = 0.83f;

    public void GetPointToSpawn(DefaultBarrier barrierType, out Vector3 position, out Quaternion rotation)
    {
        _offset = new Vector3(_lineWidth * (int)barrierType.PossibleLines[Random.Range(0, barrierType.PossibleLines.Length)], -0.15f, 3.15f);
        position = transform.position + _offset;
        rotation = Quaternion.identity;
    }
}
