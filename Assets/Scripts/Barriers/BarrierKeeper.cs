using System;
using UnityEngine;

public class BarrierKeeper : MonoBehaviour
{
    public DefaultBarrier BarrierType { get => _barrierType; }
    private DefaultBarrier _barrierType;

    public void Init(DefaultBarrier barrierType)
    {
        _barrierType = barrierType;
        gameObject.GetComponent<Renderer>().material = _barrierType.BarrierMaterial;
    }
}
