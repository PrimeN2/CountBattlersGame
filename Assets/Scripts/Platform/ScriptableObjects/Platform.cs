using UnityEngine;

[CreateAssetMenu(menuName = "Platform/Platform", fileName = "new Platform")]
public class Platform : DefaultPlatform
{
    public override void Accept(IPlatformVisiter platformVisiter, Transform transform)
    {
        platformVisiter.Visit(this, transform);
    }
    
}
