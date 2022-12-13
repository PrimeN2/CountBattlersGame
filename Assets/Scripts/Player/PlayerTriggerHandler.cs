using UnityEngine;

public class PlayerTriggerHandler : MonoBehaviour
{
    [SerializeField] private CharacterSpawner _characterSpawner;
    [SerializeField] private PlayerAlliensHandler _playerAlliensHandler;
    [SerializeField] private PlayerMovement _playerMovment;

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
                if (currentArea.IsMultiplyable)
                    _characterSpawner.Spawn(currentArea.Amount * _playerAlliensHandler.Characters.Count - _playerAlliensHandler.Characters.Count);
                else
                    _characterSpawner.Spawn(currentArea.Amount);

                Destroy(currentArea.gameObject);
                selectionBlock.IsTouched = true;
            }
        }
        else if (other.TryGetComponent(out currentBunch))
        {
            if (currentBunch.Triggered)
                return;
            var collisionPoint = other.ClosestPoint(transform.position);
            currentBunch.MoveTo(collisionPoint);

            _playerAlliensHandler.SetEnemyBunch(currentBunch);
            _playerAlliensHandler.MoveTo(collisionPoint);
            StartCoroutine(_playerMovment.MoveTo(collisionPoint));
        }
    }
}
