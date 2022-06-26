using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishHandler : MonoBehaviour
{
    public static Action OnFinished;

    private void OnTriggerEnter()
    {
        OnFinished?.Invoke();
    }
}
