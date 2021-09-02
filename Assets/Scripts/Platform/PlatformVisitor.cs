using UnityEngine;

public class PlatformVisitor : IPlatformVisiter
{
    private Vector3 _scaleChange = Vector3.one * .01f;
    private float _speedChange = .1f;

    public void Visit(Platform typePlatform, Transform transform)
    {
        if (transform.localScale.x <= 1.6f && PlayerMovement.PlayerSpeed > 0f)
        {
            //Шайтанские техники
            //ChangeSnowBallScale(transform, typePlatform.ChangingScale);
        }
    }

    private void ChangeSnowBallScale(Transform transform, int sign)
    {
        transform.gameObject.GetComponent<PlayerMovement>().TryChangeSpeed(_speedChange * sign);
        transform.localScale += _scaleChange * sign;
    }
}
