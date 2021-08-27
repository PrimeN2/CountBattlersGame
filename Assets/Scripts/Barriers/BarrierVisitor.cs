using UnityEngine;

public class BarrierVisitor : IBarrierVisitor
{
    public void Visit(CubeBarrier barrierType, GameObject player, GameObject barrier)
    {
        BarrierKeeper currentBarrier = barrier.GetComponent<BarrierKeeper>();
        if (player.GetComponent<ColorSwitcher>().NewMaterial.Equals(currentBarrier.CurrentMaterial))
        {
            Debug.Log("Equals!");
        }
        else
        {
            Debug.Log("Not Equals!");
        }
        currentBarrier.Impacted();
    }
}
