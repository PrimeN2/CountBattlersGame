using UnityEngine;

public class BarrierKeeper : MonoBehaviour
{
    public DefaultBarrier BarrierType { get => _barrierType; }
    private DefaultBarrier _barrierType;

    public EmissionMaterial CurrentMaterial { get => _currentMaterial; }
    private EmissionMaterial _currentMaterial;

    public void Init(DefaultBarrier barrierType, RoadSegment roadSegment)
    {
        _barrierType = barrierType;

        _currentMaterial = _barrierType.BarrierMaterials[
            Random.Range(0, _barrierType.BarrierMaterials.Length)];
        gameObject.GetComponent<Renderer>().material = _currentMaterial.Material;

        gameObject.transform.position = roadSegment.GetPointToSpawn(
            barrierType.PossibleLines[Random.Range(0, barrierType.PossibleLines.Length)],
            barrierType.BarrierPostion);

        gameObject.transform.localScale = _barrierType.BarrierScale;

        gameObject.transform.rotation = Quaternion.Euler(_barrierType.BarrierRotation);

    }

    public void Impacted()
    {
        gameObject.SetActive(false);
    }
}
