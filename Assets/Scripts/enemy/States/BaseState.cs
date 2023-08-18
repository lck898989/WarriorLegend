using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 状态基类
/// </summary>
public abstract class BaseState
{
    public Enemy EnemyIns;

    public BaseState(Enemy enemyInstance)
    {
        EnemyIns = enemyInstance;
    }
    /// <summary>
    /// 进入状态
    /// </summary>
    public abstract void OnEnterState();
    /// <summary>
    /// 逻辑更新
    /// </summary>
    public abstract void OnLogicUpdate();
    /// <summary>
    /// 物理更新
    /// </summary>
    public abstract void OnPhysicsUpdate();
    /// <summary>
    /// 离开状态
    /// </summary>
    public abstract void OnExitState();
}
