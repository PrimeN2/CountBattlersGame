using UnityEngine;

[CreateAssetMenu(menuName = "Platform/DryPlatform", fileName = "new DryPlatform")]
public class DryPlatform : DefaultPlatform
{
    public override void Accept(IPlatformVisiter platformVisiter, Transform transform)
    {
        platformVisiter.Visit(this, transform);
    }

}