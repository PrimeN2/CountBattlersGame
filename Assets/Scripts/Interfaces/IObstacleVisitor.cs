using UnityEngine;

public interface IObstacleVisitor
{
    void Visit(DefaultObstacle typeOfObstacle);
}
