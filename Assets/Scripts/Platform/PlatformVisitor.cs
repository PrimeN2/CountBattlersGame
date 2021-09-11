using UnityEngine;

public class PlatformVisitor : IPlatformVisiter
{
    private Vector3 _scaleChange = Vector3.one * .01f;
    private float _speedChange = .1f;

    public void Visit(Platform typePlatform, Transform transform)
    {
        PlayerMovement playerMovement = transform.gameObject.GetComponent<PlayerMovement>();

        if (transform.localScale.x <= 1.6f && playerMovement.PlayerSpeed > 0f)
        {
            //Шайтанские техники
            //ChangeSnowBallScale(transform, playerMovement, typePlatform.ChangingScale);
        }
    }

    private void ChangeSnowBallScale(Transform transform, PlayerMovement playerMovement, int sign)
    {
        playerMovement.TryChangeSpeed(_speedChange * sign);
        transform.localScale += _scaleChange * sign;
    }
}
