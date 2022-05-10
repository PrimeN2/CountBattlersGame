using System.Collections.Generic;
using UnityEngine;

public abstract class BaseGameState
{
    protected const string IS_MOVING = "IsMoving";
    protected const string ON_FINISHED= "OnFinished";

    protected static GameObject _currentPanel;

    protected readonly IGameStateSwitcher _stateSwitcher;
    protected StateArguments _stateArguments;

    protected BaseGameState(IGameStateSwitcher stateSwitcher, StateArguments stateArguments)
    {
        _stateSwitcher = stateSwitcher;
        _stateArguments = stateArguments;
    }

    public abstract void Load();
}
