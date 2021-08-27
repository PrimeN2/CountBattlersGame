using UnityEngine;

[CreateAssetMenu(menuName = "Barrier/CubeBarrier", fileName = "new CubeBarrier")]
public class CubeBarrier : DefaultBarrier
{
    public override void Accept(IBarrierVisitor obstacleVisitor, GameObject player, GameObject barrier)
    {
        obstacleVisitor.Visit(this, player, barrier);
    }
}
