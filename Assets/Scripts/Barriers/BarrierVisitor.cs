using UnityEngine;

public class BarrierVisitor : IBarrierVisitor
{
    public void Visit(CubeBarrier barrierType, GameObject player, GameObject barrier)
    {
        BarrierKeeper currentBarrier = barrier.GetComponent<BarrierKeeper>();
        if (player.GetComponent<ColorSwitcher>().CurrentColor == currentBarrier.CurrentMaterial.color)
        {
            player.GetComponent<SessionData>().IncreaseScore(1);
        }
        else
        {
            player.GetComponent<PlayerLife>().DamagePlayer(1);
        }
        currentBarrier.Impacted();
    }
}
