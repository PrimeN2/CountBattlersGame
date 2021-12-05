using UnityEngine;

public interface IPlatformVisitor
{
    void Visit(Platform typePlatform, Transform transform);
}
