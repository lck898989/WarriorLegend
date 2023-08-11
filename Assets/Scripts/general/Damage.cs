using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Damage
{
    /// <summary>
    /// 触发动画触发器名称
    /// </summary>
    public string animationTrigger = "";
    /// <summary>
    /// 组合攻击伤害
    /// </summary>
    public int damage = 0;

    /// <summary>
    /// 伤害的攻击者
    /// </summary>
    public GameObject inflictor;
    /// <summary>
    /// 组合攻击持续时间
    /// </summary>
    public float duration = 1f;
    /// <summary>
    /// 连击重置时间 超过这个时间如果没有收到连击序列 连击将会被取消
    /// </summary>
    public float comboResetTime = .5f;

    [Space(10)]
    [Header("hit collider settings")]
    public float collSize;
    public float collDistance;

    public float collHeight;

}
