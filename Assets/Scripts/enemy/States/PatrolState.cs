using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

/// <summary>
/// 巡逻状态
/// </summary>
public class PatrolState : BaseState
{
    public PatrolState(Enemy e) : base(e)
    {

    }
    public override void OnEnterState()
    {
        Debug.Log("进入巡逻状态");
        Animator animator = EnemyIns.animator;

        animator.SetBool("idle", false);
        animator.SetBool("walk", true);
        animator.SetBool("run", false);
        EnemyIns.curSpeed = EnemyIns.normalSpeed;
    }

    public override void OnExitState()
    {
        Animator animator = EnemyIns.animator;
        animator.SetBool("walk", false);
    }

    public override void OnLogicUpdate()
    {
        // 持续检测是否进入攻击范围
        if (EnemyIns.IsInAttackRange())
        {
            Debug.Log("进入攻击范围");
            // 转向玩家
            EnemyIns.State = UnitState.ATTACK;
        }
    }

    public override void OnPhysicsUpdate()
    {
    }
}
