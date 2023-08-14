using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
    /// <summary>
    /// 野猪的状态
    /// </summary>
    public UnitState State = UnitState.IDLE;

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

    // public float dirX = 1;

    // Start is called before the first frame update
    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        curSpeed = normalSpeed;
    }

    // Update is called once per frame
    public void Update()
    {

    }

    public void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        this.rb.velocity = new Vector2(-transform.localScale.x * this.curSpeed * Time.deltaTime, 0);
    }
}
