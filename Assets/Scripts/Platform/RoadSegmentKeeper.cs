using UnityEngine;

public class RoadSegmentKeeper : MonoBehaviour
{
    public void Init(Transform parent)
    {
        transform.SetParent(parent);
    }

    public Vector3 GetPointToSpawn(Vector3 position)
    {
        return position + transform.position;
    }
}
