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
        EnemyIns.curSpeed = EnemyIns.runSpeed;

        Transform player = GameManager.Instance.legendGame.Player;
        // 转向
        if (EnemyIns.transform.position.x - player.position.x > 0)
        {
            EnemyIns.transform.localScale = new Vector3(1, EnemyIns.transform.localScale.y, EnemyIns.transform.localScale.z);
        }
        if (EnemyIns.transform.position.x - player.position.x < 0)
        {
            EnemyIns.transform.localScale = new Vector3(-1, EnemyIns.transform.localScale.y, EnemyIns.transform.localScale.z);
        }
    }

    public override void OnExitState()
    {
        EnemyIns.animator.SetBool("run", false);
    }

    public override void OnLogicUpdate()
    {

    }

    public override void OnPhysicsUpdate()
    {
    }
}
