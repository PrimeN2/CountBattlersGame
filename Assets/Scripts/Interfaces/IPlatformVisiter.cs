using UnityEngine;

public interface IPlatformVisiter
{
    void Visit(SnowyPlatform typePlatform, Transform transform);
    void Visit(DryPlatform typePlatform, Transform transform);
}
