using System.Collections.Generic;
using UnityEngine;
using System;

public class RoadSegmentKeeper : MonoBehaviour
{
    public static Action<GameObject> OnSegmentOverFliew;
    public List<PlatformKeeper> PlatformKeepers;

    private void Update()
    {
        transform.position -= Vector3.forward * PlayerMovement.PlayerSpeed * Time.deltaTime;
    }
    private void LateUpdate()
    {
        if (transform.position.z < -7)
        {
            OnSegmentOverFliew?.Invoke(gameObject);
        }
    }
}
