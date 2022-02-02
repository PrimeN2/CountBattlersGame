using System.Collections;
using UnityEngine;

public abstract class DefaultBarrier : ScriptableObject
{
    public abstract void Accept(IBarrierVisitor obstacleVisitor, GameObject player, GameObject barrier);
    
    public Material[] BarrierMaterials { get => _barrierMaterials; }
    [SerializeField] protected Material[] _barrierMaterials;

    public Vector3 BarrierPostion { get => _barrierPosition; }
    [SerializeField] private Vector3 _barrierPosition;

    public Vector3 BarrierScale { get => _barrierScale; }
    [SerializeField] private Vector3 _barrierScale;

    public Vector3 BarrierRotation { get => _barrierRotation; }
    [SerializeField] private Vector3 _barrierRotation;
}
