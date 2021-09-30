using UnityEngine;

public class BarrierKeeper : MonoBehaviour
{
    public DefaultBarrier BarrierType { get; private set; }

    public EmissionMaterial CurrentMaterial { get; private set; }

    public void Init(DefaultBarrier barrierType, RoadSegmentKeeper roadSegment)
    {
        BarrierType = barrierType;

        CurrentMaterial = BarrierType.BarrierMaterials[
            Random.Range(0, BarrierType.BarrierMaterials.Length)];
        gameObject.GetComponent<Renderer>().material = CurrentMaterial.Material;

        gameObject.transform.position = roadSegment.GetPointToSpawn(
            barrierType.PossibleLines[Random.Range(0, barrierType.PossibleLines.Length)],
            barrierType.BarrierPostion);

        gameObject.transform.localScale = BarrierType.BarrierScale;

        gameObject.transform.rotation = Quaternion.Euler(BarrierType.BarrierRotation);

    }

    public void Impacted()
    {
        gameObject.SetActive(false);
    }
}
