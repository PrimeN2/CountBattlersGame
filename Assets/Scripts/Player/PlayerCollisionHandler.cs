using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    private void OnCollisionStay(Collision other)
    {
        GetPlatformType(other.gameObject)?.Accept(new PlatformVisitor(), gameObject.transform);
    }

    private DefaultPlatform GetPlatformType(GameObject platform)
    {
        return platform.GetComponent<PlatformKeeper>()?.PlatformType;
    }
}
