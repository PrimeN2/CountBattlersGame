using UnityEngine;

public interface IBarrierVisitor
{
    void Visit(CubeBarrier barrierType, GameObject player, GameObject barrier);
}
