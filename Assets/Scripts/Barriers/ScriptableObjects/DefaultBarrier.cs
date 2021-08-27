using System.Collections;
using UnityEngine;

public abstract class DefaultBarrier : ScriptableObject
{
    public abstract void Accept(IBarrierVisitor obstacleVisitor, GameObject player, GameObject barrier);
    
    public EmissionMaterial[] BarrierMaterials { get => _barrierMaterials; }
    [SerializeField] protected EmissionMaterial[] _barrierMaterials;

    public Vector3 BarrierPostion { get => _barrierPosition; }
    [SerializeField] private Vector3 _barrierPosition;

    public Vector3 BarrierScale { get => _barrierScale; }
    [SerializeField] private Vector3 _barrierScale;

    public Vector3 BarrierRotation { get => _barrierRotation; }
    [SerializeField] private Vector3 _barrierRotation;

    public LineSwitcher.Lines[] PossibleLines { get => _possibleLines; }
    [SerializeField] protected LineSwitcher.Lines[] _possibleLines;

}
