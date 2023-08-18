using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : BaseState
{
    public DeadState(Enemy e) : base(e)
    {

    }
    public override void OnEnterState()
    {
        Animator animator = EnemyIns.animator;
        animator.SetBool("dead", true);
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
