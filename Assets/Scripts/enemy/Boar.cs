using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using Unity.VisualScripting;
using UnityEngine;


/// <summary>
/// 野猪类
/// </summary>
public class Boar : Enemy
{
    /// <summary>
    /// 猪的攻击范围
    /// </summary>
    public float attackDistance = 1f;

    public override void SetupAllState()
    {
        base.SetupAllState();
        StateMap.Add(UnitState.IDLE, new IdleState(this));
        StateMap.Add(UnitState.PATROL, new PatrolState(this));
        StateMap.Add(UnitState.DEAD, new DeadState(this));
        StateMap.Add(UnitState.ATTACK, new AttackState(this));
        StateMap.Add(UnitState.HIT, new HitState(this));
    }

    public override bool IsInAttackRange()
    {
        Transform player = GameManager.Instance.legendGame.Player;
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("player").transform;
        }
        float distance = Vector3.Distance(player.position, transform.position);
        if (distance > attackDistance)
        {
            return false;
        }

        // 转向
        if (transform.position.x - player.position.x > 0)
        {
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        }
        if (transform.position.x - player.position.x < 0)
        {
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        }
        return true;
    }

}
