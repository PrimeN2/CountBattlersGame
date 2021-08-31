using UnityEngine;

public class BarrierVisitor : IBarrierVisitor
{
    public void Visit(CubeBarrier barrierType, GameObject player, GameObject barrier)
    {
        BarrierKeeper currentBarrier = barrier.GetComponent<BarrierKeeper>();
        if (player.GetComponent<ColorSwitcher>().NewMaterial.Equals(currentBarrier.CurrentMaterial))
        {
            player.GetComponent<SessionData>().IncreaseScore(1);
        }
        else
        {
            player.GetComponent<SessionData>().DamagePlayer(1);
        }
        currentBarrier.Impacted();
    }
}
