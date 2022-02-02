using UnityEngine;

public class BarrierKeeper : MonoBehaviour
{
    public DefaultBarrier BarrierType { get; private set; }

    public Material CurrentMaterial { get; private set; }

    public void Init(DefaultBarrier barrierType, RoadSegmentKeeper roadSegment)
    {
        BarrierType = barrierType;

        CurrentMaterial = BarrierType.BarrierMaterials[
            Random.Range(0, BarrierType.BarrierMaterials.Length)];
        gameObject.GetComponent<Renderer>().material = CurrentMaterial;

        gameObject.transform.position = roadSegment.GetPointToSpawn(barrierType.BarrierPostion);

        gameObject.transform.localScale = BarrierType.BarrierScale;

        gameObject.transform.rotation = Quaternion.Euler(BarrierType.BarrierRotation);

    }

    public void Impacted()
    {
        gameObject.SetActive(false);
    }
}
