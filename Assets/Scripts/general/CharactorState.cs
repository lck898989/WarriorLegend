using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum UnitState
{
    IDLE,
    WALK,
    JUMP,
    RUN,
    ///
    /// 攻击
    /// 
    ATTACK,
    HIT,
    DEAD,
    STANDUP,
    DEFEND,
    /// <summary>
    /// 巡逻
    /// </summary>
    PATROL
}

public class CharactorState : MonoBehaviour
{
    // Start is called before the first frame update
    public UnitState curState = UnitState.IDLE;

    public void SetState(UnitState state)
    {
        this.curState = state;
    }
}
