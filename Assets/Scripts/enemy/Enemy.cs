using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{

    public bool canWait = false;
    public Vector2 startPoint;
    public Vector2 endPoint;

    public Charactor charactor;
    /// <summary>
    /// 野猪的状态
    /// </summary>
    public UnitState _State = UnitState.IDLE;

    public UnitState State
    {
        get => _State;
        set
        {
            if (_State != value)
            {
                _State = value;
                switch (_State)
                {
                    case UnitState.IDLE:
                        CurState = StateMap[UnitState.IDLE];
                        CurState.OnEnterState();
                        break;
                    case UnitState.PATROL:
                        CurState = StateMap[UnitState.PATROL];
                        CurState.OnEnterState();
                        break;
                    case UnitState.RUN:
                        CurState = StateMap[UnitState.RUN];
                        CurState.OnEnterState();
                        break;
                    case UnitState.DEAD:
                        CurState = StateMap[UnitState.DEAD];
                        CurState.OnEnterState();
                        break;
                    case UnitState.ATTACK:
                        CurState = StateMap[UnitState.ATTACK];
                        CurState.OnEnterState();
                        break;
                    case UnitState.HIT:
                        CurState = StateMap[UnitState.HIT];
                        CurState.OnEnterState();
                        break;
                    default:
                        break;
                }
            }
        }
    }

    /// <summary>
    /// 状态机map
    /// </summary>
    /// <typeparam name="UnitState"></typeparam>
    /// <typeparam name="BaseState"></typeparam>
    /// <returns></returns>
    public Dictionary<UnitState, BaseState> StateMap = new Dictionary<UnitState, BaseState>();

    public BaseState CurState;
    public Rigidbody2D rb;
    public Animator animator;
    /// <summary>
    /// 当前野猪的速度
    /// </summary>
    public float curSpeed = 0.0f;
    /// <summary>
    /// 追击玩家的速度
    /// </summary>
    public float runSpeed = 200f;
    /// <summary>
    /// 正常走路的速度
    /// </summary>
    public float normalSpeed = 100f;

    /// <summary>
    /// 等待时间
    /// </summary>
    public float waitTime = 1f;
    /// <summary>
    /// 等待时间计数器
    /// </summary>
    public float waitTimeCounter = 0f;

    /// <summary>
    /// 是否处于等待状态
    /// </summary>
    public bool isWait = false;

    /// <summary>
    /// 是否被击中
    /// </summary>
    public bool isHit = false;

    public Transform player;

    // public float dirX = 1;

    void Awake()
    {
        charactor = GetComponent<Charactor>();
        SetupAllState();
    }

    /// <summary>
    /// 安装所有状态
    /// </summary>
    public virtual void SetupAllState()
    {

    }

    public virtual bool IsInAttackRange()
    {
        return false;
    }

    // Start is called before the first frame update
    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        curSpeed = normalSpeed;
        // 默认进入巡逻状态
        State = UnitState.PATROL;
    }

    private void OnEnable()
    {
        CurState?.OnEnterState();
    }

    // Update is called once per frame
    public void Update()
    {
        CommonLogicUpdate();
        Debug.Log("curState is " + CurState);
        CurState?.OnLogicUpdate();
    }

    public void CommonLogicUpdate()
    {
        Transform trs = transform;

        if (trs.position.x <= endPoint.x || trs.position.x >= startPoint.x)
        {
            if (!isWait)
            {
                isWait = true;
                if (canWait)
                {
                    State = UnitState.IDLE;
                }
            }
            // 强制拉回
            if (trs.position.x <= endPoint.x)
            {
                trs.position = new Vector2(endPoint.x, trs.position.y);
            }
            else
            {
                trs.position = new Vector2(startPoint.x, trs.position.y);
            }
            if (!canWait)
            {
                // 不等的话立刻转向
                float targetX = -trs.localScale.x;
                trs.localScale = new Vector3(targetX, trs.localScale.y, trs.localScale.z);
            }
        }
        else
        {
            isWait = false;
            if (canWait)
            {
                waitTimeCounter = 0;
            }
            if (State != UnitState.PATROL)
                State = UnitState.PATROL;
        }

        if (canWait && isWait)
        {
            // 等待waitTime之后再转向
            waitTimeCounter += Time.deltaTime;
            if (waitTimeCounter >= waitTime)
            {
                isWait = false;
                waitTimeCounter = 0;
                // 转向
                float targetX = -trs.localScale.x;
                trs.localScale = new Vector3(targetX, trs.localScale.y, trs.localScale.z);
                State = UnitState.PATROL;
            }
        }
    }

    public void OnTakeDamage(Transform Attacker)
    {
        Debug.Log("野猪受到攻击转向");
        if (Attacker.position.x - transform.position.x > 0)
        {
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        }
        State = UnitState.HIT;
        // 野猪弹开一段距离
        StartCoroutine(OnHit());
    }

    private IEnumerator OnHit()
    {
        yield return new WaitForSeconds(0.8f);
        isHit = false;
        // 继续巡逻
        State = UnitState.PATROL;
    }

    public void OnDead(Transform Attacker)
    {
        State = UnitState.DEAD;
    }

    public void DeadDestroy()
    {
        Destroy(this.gameObject);
    }

    public void FixedUpdate()
    {
        CommonFixedUpdate();
        CurState?.OnPhysicsUpdate();
    }

    public void CommonFixedUpdate()
    {
        if (!isHit && !charactor.IsDead)
        {
            // 继续巡逻
            Move();
        }
    }

    public virtual void Move()
    {
        this.rb.velocity = new Vector2(-transform.localScale.x * this.curSpeed * Time.deltaTime, 0);
    }
}
