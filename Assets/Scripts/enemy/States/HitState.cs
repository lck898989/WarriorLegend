using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 受击状态
/// </summary>
public class HitState : BaseState
{
    public Transform Attacker;
    public HitState(Enemy e) : base(e)
    {

    }

    public override void OnEnterState()
    {
        EnemyIns.animator.SetTrigger("hit");
        EnemyIns.isHit = true;
    }

    public override void OnExitState()
    {
    }

    public override void OnLogicUpdate()
    {
    }

    public override void OnPhysicsUpdate()
    {
        Debug.LogWarning("hit fixed update....");
        if (EnemyIns.isHit)
        {
            EnemyIns.rb.AddForce(new Vector2(EnemyIns.transform.localScale.x * 2, 0), ForceMode2D.Impulse);
        }
    }
}
