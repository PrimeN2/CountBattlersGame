using UnityEngine;

public class PlayerTriggerHandler : MonoBehaviour
{
    [SerializeField] private CharacterSpawner _characterSpawner;

    private void OnTriggerEnter(Collider other)
    {
        //other.gameObject.GetComponent<BarrierKeeper>().BarrierType.Accept(new BarrierVisitor(), gameObject, other.gameObject);
        SelectionAreaKeeper currentArea;
        if (other.TryGetComponent(out currentArea))
        {
            SelectionBlockKeeper selectionBlock = currentArea.GetBlock();
            if (selectionBlock.IsTouched == false)
            {
                _characterSpawner.Spawn(6);
                selectionBlock.IsTouched = true;
            }
        }
    }
}
