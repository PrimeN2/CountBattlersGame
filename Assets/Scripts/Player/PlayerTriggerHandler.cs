using System.Collections;
using UnityEngine;

public class PlayerTriggerHandler : MonoBehaviour
{
    [SerializeField] private CharacterSpawner _characterSpawner;
    [SerializeField] private PlayerAlliensHandler _playerAlliensHandler;

    private void OnTriggerEnter(Collider other)
    {
        //other.gameObject.GetComponent<BarrierKeeper>().BarrierType.Accept(new BarrierVisitor(), gameObject, other.gameObject);
        SelectionAreaKeeper currentArea;
        BunchHandler currentBunch;

        if (other.TryGetComponent(out currentArea))
        {
            SelectionBlockKeeper selectionBlock = currentArea.GetBlock();
            if (selectionBlock.IsTouched == false)
            {
                _characterSpawner.Spawn(6, false);
                selectionBlock.IsTouched = true;
            }
        }
        else if (other.TryGetComponent(out currentBunch))
        {
            if (currentBunch.Triggered)
                return;
            var collisionPoint = other.ClosestPoint(transform.position);
            currentBunch.MoveTo(collisionPoint);
            _playerAlliensHandler.MoveTo(collisionPoint);
            _playerAlliensHandler.SetEnemyBunch(currentBunch);
        }
    }
}
