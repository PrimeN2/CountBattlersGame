using UnityEngine;

public class PlatformVisitor : IPlatformVisiter
{
    private Vector3 _scaleChange = Vector3.one * .01f;
    private float _speedChange = .1f;
    public void Visit(SnowyPlatform typePlatform, Transform transform)
    {
        if (transform.localScale.x <= 1.6f && PlayerMovement.PlayerSpeed > 0f)
        {
            ChangeSnowBallScale(transform, typePlatform.ChangingScale);
        }
    }
    public void Visit(DryPlatform typePlatform, Transform transform)
    {
        if (transform.localScale.x >= .3f && PlayerMovement.PlayerSpeed < 15f)
        {
            ChangeSnowBallScale(transform, typePlatform.ChangingScale);
        }
    }
    private void ChangeSnowBallScale(Transform transform, int sign)
    {
        PlayerMovement.PlayerSpeed -= _speedChange * sign;
        transform.localScale += _scaleChange * sign;
    }
}
