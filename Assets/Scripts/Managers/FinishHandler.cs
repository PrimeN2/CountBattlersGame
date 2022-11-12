using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishHandler : MonoBehaviour
{
    public static Action<int> OnFinished;

    private void OnTriggerEnter(Collider collider)
    {
        PlayerAlliensHandler player;
        if (collider.gameObject.TryGetComponent<PlayerAlliensHandler>(out player))
            OnFinished?.Invoke(player.Characters.Count);
    }
}
