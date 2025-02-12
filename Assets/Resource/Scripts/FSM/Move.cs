using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : BaseState
{
    protected Player player;

    public Move(StateMachine stateMachine) : base("Move", stateMachine)
    {
        player = (Player)stateMachine;

    }

    public override void Enter()
    {
        base.Enter();
        player.PlayerAnimator.SetBool("Movebool", true);


        player.TurnRotation();

    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();



        player.MoveToTable();



        if (player.Waiting)
        {
            _stateMachine.ChangeState(player.WaitingState);
            
        }


        if (player.FoodName == null)
        {   
            _stateMachine.ChangeState(player.IdleState);
        }

        

        

    }

    public override void Exit()
    {

    }

}
