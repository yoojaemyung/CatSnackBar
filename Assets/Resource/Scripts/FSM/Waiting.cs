using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waiting : BaseState
{
    protected Player player;

    public Waiting(StateMachine stateMachine) : base("Waiting", stateMachine)
    {
        player = (Player)stateMachine;

    }

    public override void Enter()
    {
        base.Enter();

        player.PlayerAnimator.SetBool("Movebool", false);
        player.PlayerAnimator.SetBool("Orderbool", false);

    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        player.FindCookingPlace(player.FoodName);

        if (!player.Waiting)
        {
            _stateMachine.ChangeState(player.MoveState);
        }




    }

    public override void Exit()
    {

    }


}


