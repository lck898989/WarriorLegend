using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : BaseState
{
    public IdleState(Enemy enemyInstance) : base(enemyInstance)
    {
    }

    public override void OnEnterState()
    {
        Animator animator = EnemyIns.animator;

        animator.SetBool("idle", true);
        animator.SetBool("walk", false);
        animator.SetBool("run", false);
    }

    public override void OnExitState()
    {
    }

    public override void OnLogicUpdate()
    {

    }

    public override void OnPhysicsUpdate()
    {

    }
}
