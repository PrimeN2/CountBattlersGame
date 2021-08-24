using UnityEngine;

public interface IBarrierVisitor
{
    void Visit(DefaultBarrier typeOfObstacle);
}
