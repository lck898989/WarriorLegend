using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Attack
{
    /// <summary>
    ///  伤害值
    /// </summary>
    public int damage;
    /// <summary>
    /// 伤害范围
    /// </summary>
    public float damageRange;
}

/// <summary>
/// 角色类
/// </summary>
public class Charactor : MonoBehaviour
{
    /// <summary>
    /// 角色的最大血量
    /// </summary>
    public float maxHp = 100.0f;
    /// <summary>
    ///  功绩属性
    /// </summary>
    public Attack attackInstance;

    /// <summary>
    /// 是否处于无敌状态
    /// </summary>
    public bool invincible = false;
    /// <summary>
    /// 无敌时间
    /// </summary>
    public float invincibleTime = 1.0f;

    /// <summary>
    /// 无敌计时器
    /// </summary>
    public float invincibleCounter = 0f;

    /// <summary>
    /// 受到伤害时触发的事件
    /// </summary>
    public UnityEvent<Transform> onTakeDamage;

    public UnityEvent<Transform> onDead;

    /// <summary>
    /// 角色的当前血量
    /// </summary>
    public float hp = 100;

    private bool isDead = false;

    /// <summary>
    /// 是否死亡
    /// </summary>
    /// <value></value>
    public bool IsDead
    {
        get
        {
            return hp <= 0;
        }
    }


    public float Hp
    {
        get => hp;
        set
        {
            hp = value;
            if (hp <= 0)
            {
                hp = 0;
                this.Die();
            }
        }
    }

    public void Die()
    {
        Debug.Log("死亡");
        this.onDead?.Invoke(this.transform);

    }

    /// <summary>
    /// 受到伤害
    /// </summary>
    /// <param name="damage">伤害值</param>
    public void TakeDamage(GameObject other, float damage)
    {
        if (invincible) return;
        this.Hp -= damage;
        if (this.Hp >= 0)
        {
            this.triggerInvincible();
            // 启动事件触发器
            this.onTakeDamage?.Invoke(other.transform);
        }

    }

    /// <summary>
    /// 触发无敌状态
    /// </summary>
    public void triggerInvincible()
    {
        this.invincible = true;
    }

    private void OnDisable()
    {

    }

    void Update()
    {
        if (invincible)
        {
            invincibleCounter += Time.deltaTime;
            if (invincibleCounter >= invincibleTime)
            {
                invincible = false;
                invincibleCounter = 0;
            }
        }
    }
}
