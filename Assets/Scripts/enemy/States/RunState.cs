using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : BaseState
{
    public RunState(Enemy e) : base(e)
    {

    }
    public override void OnEnterState()
    {
        Animator animator = EnemyIns.animator;
        animator.SetBool("idle", false);
        animator.SetBool("walk", false);
        animator.SetBool("run", true);
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
