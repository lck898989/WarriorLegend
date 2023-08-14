using System.Collections;
using System.Collections.Generic;
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

        }
        if (charactorState.curState == UnitState.ATTACK && !continueAttackCombo && playerController.pc.isGround)
        {
            if (attackIndex < attackCombo.Length - 1)
            {
                // 已经处于攻击状态，且不是连续攻击 将连续攻击状态打开
                continueAttackCombo = true;
                return;
            }
        }
    }

    public void doAttack(Damage damage, UnitState state)
    {
        Debug.Log("animationTrigger is " + damage.animationTrigger);
        lastAttackTime = Time.time;
        lastAttack = damage;
        lastAttack.inflictor = this.gameObject;
        charactorState.SetState(state);

        playerController.playerAnimation.setTrigger(damage.animationTrigger);
        Invoke("Ready", damage.duration);

    }

    public void Ready()
    {

        Debug.LogWarning("Ready");
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
    }

    // Update is called once per frame
    void Update()
    {

    }
}
