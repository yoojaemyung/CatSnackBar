using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    protected BaseState _currentState;
    void Start()
    {
        _currentState = GetState();
        if (_currentState != null)
            _currentState.Enter();
    }
    public void Update()
    {
        if (_currentState != null && UI_Manager.Instance.FirstCheck)
            _currentState.UpdateLogic();
    }
    public void ChangeState(BaseState newState)
    {
        _currentState.Exit();
        _currentState = newState;
        _currentState.Enter();    
    }
    protected virtual BaseState GetState()
    {
        return new Idle(this); // 초기 Idle 설정
    }
}
