using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : BaseState
{
    protected Player player;
    public Idle(StateMachine stateMachine) : base("Idle", stateMachine)
    {
        player = (Player)stateMachine;     
    }
    public override void Enter()
    {
        base.Enter();
        player.transform.rotation = Quaternion.Euler(0, 0, 0); // idle 애니메이션 으로 인한 방향 초기화
    }
    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if (Manager.FoodManager.WaitingTablePos.Count >= 1)
        {
            player.TargetTable = Manager.FoodManager.WaitingTablePos[0].gameObject;

            Manager.FoodManager.WaitingTablePos.RemoveAt(0);

            player.TargetPos = player.TargetTable.transform; // 주문이 들어온지 체크 후 대기중이면 이동.

            player.FoodName = Manager.FoodManager.WaitingOrder[0];

            Manager.FoodManager.WaitingOrder.RemoveAt(0);
            
            _stateMachine.ChangeState(player.MoveState);
        }
        else if(player.Waiting)
        {
            player.FindCookingPlace(player.FoodName);

            if (!player.Waiting)
                _stateMachine.ChangeState(player.MoveState);
               
        }
        else player.PlayerAnimator.SetBool("Movebool", false);
    }
    public override void Exit()
    {
        
    }

}
