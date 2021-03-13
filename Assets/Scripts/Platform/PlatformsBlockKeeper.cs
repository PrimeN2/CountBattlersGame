using System.Collections.Generic;
using UnityEngine;
using System;

public class PlatformsBlockKeeper : MonoBehaviour
{
    public static Action<GameObject> OnPlatformOverFliew;
    public List<PlatformKeeper> PlatformKeepers;

    private void Update()
    {
        transform.position -= Vector3.forward * PlayerMovement.PlayerSpeed * Time.deltaTime;
    }
    private void LateUpdate()
    {
        if (transform.position.z < -7)
        {
            OnPlatformOverFliew?.Invoke(gameObject);
        }
    }
}
