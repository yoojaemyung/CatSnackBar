using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseState
{
    public string _name;
    protected StateMachine _stateMachine;

    protected BaseState(string name ,StateMachine stateMachine)
    {

        _name = name;
        _stateMachine = stateMachine;
    }

    public virtual void Enter() { }
    public virtual void UpdateLogic() { }
    public virtual void Exit() { }

}
