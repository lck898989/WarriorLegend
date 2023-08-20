using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 追击状态
/// </summary>
public class AttackState : BaseState
{
    public AttackState(Enemy e) : base(e)
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
        EnemyIns.animator.SetBool("run",false);
    }

    public override void OnLogicUpdate()
    {
    }

    public override void OnPhysicsUpdate()
    {
    }
}
