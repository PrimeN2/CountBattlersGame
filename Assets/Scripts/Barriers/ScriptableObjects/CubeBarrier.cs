using UnityEngine;

[CreateAssetMenu(menuName = "Barrier/CubeBarrier", fileName = "new CubeBarrier")]
public class CubeBarrier : DefaultBarrier
{
    public override void Accept(IBarrierVisitor obstacleVisitor)
    {
        obstacleVisitor.Visit(this);
    }

    public override void Spawn()
    {
        throw new System.NotImplementedException();
    }

    public override void Impacted()
    {
        base.Impacted();
    }
}
