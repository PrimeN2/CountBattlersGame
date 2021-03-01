using UnityEngine;

public abstract class DefaultObstacle : MonoBehaviour
{
    public abstract void Accept(IObstacleVisitor obstacleVisitor);
}
