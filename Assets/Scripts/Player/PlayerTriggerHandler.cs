using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggerHandler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //other.gameObject.GetComponent<BarrierKeeper>().BarrierType.Accept(new BarrierVisitor(), gameObject, other.gameObject);
    }
}
