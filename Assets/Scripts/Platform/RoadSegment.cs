using UnityEngine;

public class RoadSegment : MonoBehaviour
{
    private Vector3 _position;

    public Vector3 GetPointToSpawn(LineSwitcher.Lines line, Vector3 offset)
    {
        _position = new Vector3(offset.x * (int)line, offset.y, offset.z) + transform.position;
        return _position;
    }
}
