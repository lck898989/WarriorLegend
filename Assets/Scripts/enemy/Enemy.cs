using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{

    public bool canWait = false;
    public Vector2 startPoint;
    public Vector2 endPoint;
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
                        animator.SetBool("idle", true);
                        animator.SetBool("walk", false);
                        animator.SetBool("run", false);
                        break;
                    case UnitState.WALK:
                        animator.SetBool("idle", false);
                        animator.SetBool("walk", true);
                        animator.SetBool("run", false);
                        break;
                    case UnitState.RUN:
                        animator.SetBool("idle", false);
                        animator.SetBool("walk", false);
                        animator.SetBool("run", true);
                        break;
                    case UnitState.DEAD:
                        animator.SetBool("dead", true);
                        break;
                    default:
                        break;
                }
            }
        }
    }

    protected Rigidbody2D rb;
    protected Animator animator;

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

    // public float dirX = 1;

    // Start is called before the first frame update
    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        curSpeed = normalSpeed;

        State = UnitState.WALK;
    }

    // Update is called once per frame
    public void Update()
    {
        if (transform.position.x <= endPoint.x || transform.position.x >= startPoint.x)
        {
            if (!isWait)
            {
                this.isWait = true;
                if (canWait)
                {
                    State = UnitState.IDLE;
                }
            }
            // 强制拉回
            if (transform.position.x <= endPoint.x)
            {
                Debug.Log("到达左边界");
                transform.position = new Vector2(endPoint.x, transform.position.y);
            }
            else
            {
                Debug.Log("到达右边界");
                transform.position = new Vector2(startPoint.x, transform.position.y);
            }
            if (!canWait)
            {
                // 不等的话立刻转向
                float targetX = -transform.localScale.x;
                transform.localScale = new Vector3(targetX, transform.localScale.y, transform.localScale.z);
            }
        }
        else
        {
            isWait = false;
            if (canWait)
            {
                waitTimeCounter = 0;
            }
            if (State != UnitState.WALK)
                State = UnitState.WALK;
        }

        if (canWait && isWait)
        {
            Debug.Log("waiting...");
            // 等待waitTime之后再转向
            waitTimeCounter += Time.deltaTime;
            if (waitTimeCounter >= waitTime)
            {
                isWait = false;
                waitTimeCounter = 0;
                Debug.Log("转向。。" + transform.localScale.x);
                // 转向
                float targetX = -transform.localScale.x;
                transform.localScale = new Vector3(targetX, transform.localScale.y, transform.localScale.z);
                State = UnitState.WALK;
            }
        }
    }



    public void FixedUpdate()
    {

        Move();
    }

    public virtual void Move()
    {
        this.rb.velocity = new Vector2(-transform.localScale.x * this.curSpeed * Time.deltaTime, 0);
    }
}
