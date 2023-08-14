using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    public Damage[] attackCombo;

    public Damage lastAttack;

    public float lastAttackTime;

    public PlayerController playerController;
    public CharactorState charactorState;

    public int attackIndex = 0;

    public bool continueAttackCombo;

    public int drawAttackIndex = 0;

    void Awake()
    {
        playerController = GetComponent<PlayerController>();
        charactorState = GetComponent<CharactorState>();
    }

    public void combatEvent()
    {
        Debug.LogWarning("state is " + charactorState.curState);
        if (charactorState.curState != UnitState.ATTACK)
        {
            // 是否在攻击窗口内
            bool insideComboWindow = (lastAttack != null && (Time.time < (lastAttackTime + lastAttack.duration + lastAttack.comboResetTime)));

            if (insideComboWindow && !continueAttackCombo && attackIndex < attackCombo.Length - 1)
            {
                // 继续攻击
                attackIndex++;
            }
            else
            {
                // 重置攻击
                attackIndex = 0;
            }

            doAttack(attackCombo[attackIndex], UnitState.ATTACK);
            return;
        }

        if (charactorState.curState == UnitState.ATTACK && !continueAttackCombo && playerController.pc.isGround)
        {
            if (attackIndex < attackCombo.Length - 1)
            {
                // 已经处于攻击状态，且不是连续攻击 将连续攻击状态打开
                continueAttackCombo = true;
            }
            return;
        }
    }

    public void doAttack(Damage damage, UnitState state)
    {
        Debug.Log("animationTrigger is " + damage.animationTrigger);
        if (damage.animationTrigger == "attack3")
        {
            Debug.Log("attack3");
        }
        lastAttackTime = Time.time;
        lastAttack = damage;
        lastAttack.inflictor = this.gameObject;
        charactorState.SetState(state);
        playerController.playerAnimation.setTrigger(damage.animationTrigger);
        Invoke("Ready", damage.duration);
    }

    public void Ready()
    {

        Debug.LogWarning("Ready" + " continuecombo is " + continueAttackCombo);
        if (continueAttackCombo)
        {
            // 重置是否继续连招动作 因为这个时候玩家没有触发攻击动作
            continueAttackCombo = false;
            if (attackIndex < attackCombo.Length - 1)
            {
                attackIndex++;
            }
            else
            {
                attackIndex = 0;
            }

            Debug.LogWarning("连招" + attackCombo[attackIndex].animationTrigger);

            if (attackCombo[attackIndex] != null && attackCombo[attackIndex].animationTrigger.Length > 0)
            {
                doAttack(attackCombo[attackIndex], UnitState.ATTACK);
            }
        }

        // 重置玩家状态
        charactorState.SetState(UnitState.IDLE);
    }

    public void AttackHit(int attackIndex)
    {
        drawAttackIndex = attackIndex;

        Damage attack1Damage = attackCombo[attackIndex];
        attack1Damage.inflictor = this.gameObject;

        float distance = attack1Damage.collDistance;
        float height = attack1Damage.collHeight;
        float size = attack1Damage.collSize;

        Vector2 dir = playerController.lastInputDir;
        Vector3 direction = new Vector3(dir.x, 0, 0);

        Vector3 center = transform.position + direction * distance + transform.up * height;
        Vector3 boxSize = new Vector3(size, size, size);

        Collider2D[] colliders = Physics2D.OverlapBoxAll(new Vector2(center.x, center.y), new Vector2(size, size), 0, LayerMask.GetMask("enemy"));
        foreach (Collider2D collider in colliders)
        {
            Debug.Log("hit " + collider.gameObject.name);
            // 对敌人造成伤害
            collider.gameObject.GetComponent<Charactor>().TakeDamage(gameObject, attack1Damage.damage);
        }
    }

    void OnDrawGizmos()
    {
        if (playerController == null) return;

        Damage attack1Damage = attackCombo[attackIndex];
        attack1Damage.inflictor = this.gameObject;
        float distance = attack1Damage.collDistance;
        float height = attack1Damage.collHeight;
        float size = attack1Damage.collSize;

        Gizmos.color = Color.red;

        Vector2 dir = playerController.lastInputDir;
        Vector3 direction = new Vector3(dir.x, 0, 0);

        Vector3 center = transform.position + direction * distance + transform.up * height;
        // Collider[] colliders = Physics.OverlapBox(center, new Vector3(size, size, size), transform.rotation, LayerMask.GetMask("enemy"));
        Gizmos.DrawWireCube(center, new Vector3(size, size, size));
    }

    // Update is called once per frame
    void Update()
    {

    }
}
