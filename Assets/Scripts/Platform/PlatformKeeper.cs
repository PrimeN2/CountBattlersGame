using UnityEngine;

public class PlatformKeeper : MonoBehaviour
{
    public DefaultPlatform PlatformType { get => _platformType; }
    private DefaultPlatform _platformType;

    public void Init(DefaultPlatform platformType)
    {
        _platformType = platformType;
        gameObject.GetComponent<MeshRenderer>().material = platformType.Material;
    }
}
