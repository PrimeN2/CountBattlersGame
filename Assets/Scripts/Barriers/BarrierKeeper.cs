using UnityEngine;

public class BarrierKeeper : MonoBehaviour
{
    public DefaultBarrier BarrierType { get; private set; }

    public Material CurrentMaterial { get; private set; }

    private int _distanceMultiplier = 5;

    public void Init(DefaultBarrier barrierType, RoadSegmentKeeper roadSegment, int current)
    {
        BarrierType = barrierType;

        CurrentMaterial = BarrierType.BarrierMaterials[
            Random.Range(0, BarrierType.BarrierMaterials.Length)];
        gameObject.GetComponent<Renderer>().material = CurrentMaterial;

        gameObject.transform.position = roadSegment.GetPlatformEnd() + 
            new Vector3(barrierType.BarrierPostion.x * Random.Range(-1, 2), barrierType.BarrierPostion.y, 
            barrierType.BarrierPostion.z + current * _distanceMultiplier);

        gameObject.transform.localScale = barrierType.BarrierScale;

        gameObject.transform.rotation = Quaternion.Euler(BarrierType.BarrierRotation);

    }

    public void Impacted()
    {
        gameObject.SetActive(false);
    }
}
