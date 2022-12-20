using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightState : BaseGameState
{
    public FightState(IGameStateSwitcher stateSwitcher, StateArguments stateArguments)
    : base(stateSwitcher, stateArguments)
    {

    }


    public override void Load()
    {

        _stateArguments._playerMovement.StopMoving();
        _stateArguments._inputManager.DeinitInputHandle(false);
    }
}
