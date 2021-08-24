using System.Collections;
using UnityEngine;

public abstract class DefaultBarrier : ScriptableObject
{
    public abstract void Accept(IBarrierVisitor obstacleVisitor);

    public virtual void Setup() { }

    public abstract void Spawn();

    public virtual void Impacted()
    {

    }

    public GameObject BarrierPrefab { get => _barrierPrefab; }
    [SerializeField] protected GameObject _barrierPrefab;
    
    public Material BarrierMaterial { get => _barrierMaterial; }
    [SerializeField] protected Material _barrierMaterial;

    public LineSwitcher.Lines[] PossibleLines { get => _possibleLines; }
    [SerializeField] protected LineSwitcher.Lines[] _possibleLines;

}
