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
    ATTACK,
    HIT,
    DEAD,
    STANDUP,
    DEFEND,
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
