using UnityEngine;

public class BarrierIdentifier
{
    private DefaultBarrier[] _barrierTypes;

    public BarrierIdentifier(DefaultBarrier[] barrierTypes)
    {
        _barrierTypes = barrierTypes;
    } 

    public void DefineBarrier(out DefaultBarrier barrier)
    {
        barrier = _barrierTypes[Random.Range(0, _barrierTypes.Length)];
    }
}
