using UnityEngine;

[CreateAssetMenu(menuName = "Platform/SnowyPlatform", fileName = "new SnowyPlatform")]
public class SnowyPlatform : DefaultPlatform
{
    public override void Accept(IPlatformVisiter platformVisiter, Transform transform)
    {
        platformVisiter.Visit(this, transform);
    }
    
}
