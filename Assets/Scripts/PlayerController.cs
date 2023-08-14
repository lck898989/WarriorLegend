using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PlayerInput playerInput = null;

    public Vector2 inputDirection = new Vector2(1, 0);

    public Vector2 lastInputDir = new Vector2(1, 0);

    public PlayerAnimation playerAnimation;

    public PlayerCombat playerCombat;

    public CharactorState charactorState;

    private CapsuleCollider2D cc2d;
    public PhysicsCheck pc;

    private Rigidbody2D rb;

    [Space(10)]
    [Header("运动参数")]
    public float speed = 400;

    public float runSpeed;
    public float walkSpeed => speed / 2.5f;
    public float jumpForce = 0.3f;

    private Vector2 originOffset = Vector2.zero;
    private Vector2 originSize = Vector2.zero;

    private Charactor charactor;

    private bool isCrouch = false;

    public bool isDead = false;

    [Space(10)]
    [Header("弹开参数")]
    public float bounceForce = 1f;
    public bool isBounce = false;
    public Vector2 bounceDirection = Vector2.zero;


    [Space(10)]
    [Header("攻击参数")]
    public bool isAttack = false;
    [Tooltip("攻击重置窗口时间")]
    public float attackResetTime = 1f;

    public void Awake()
    {


        this.rb = this.GetComponent<Rigidbody2D>();
        this.playerInput = new PlayerInput();
        this.pc = GetComponent<PhysicsCheck>();
        this.cc2d = GetComponent<CapsuleCollider2D>();
        playerAnimation = GetComponent<PlayerAnimation>();
        charactor = GetComponent<Charactor>();
        playerCombat = GetComponent<PlayerCombat>();
        charactorState = GetComponent<CharactorState>();

        this.originOffset = cc2d.offset;
        this.originSize = cc2d.size;

        this.runSpeed = speed;
        // performed 持续
        this.playerInput.GamePlay.Jump.started += Jump;
        // 攻击
        this.playerInput.GamePlay.Attack.started += Attack;

        this.rb.AddForce(bounceForce * new Vector2(1, 0), ForceMode2D.Impulse);

        #region 走路事件
        this.playerInput.GamePlay.Move.started += (InputAction.CallbackContext ctx) =>
        {
            Debug.Log("dir is " + inputDirection);
        };
        // 持续按走路键
        this.playerInput.GamePlay.Walk.performed += (InputAction.CallbackContext ctx) =>
        {
            Debug.Log("持续按走路键");
            if (pc.isGround)
            {
                this.speed = this.walkSpeed;
            }
        };

        // 松开按走路键
        this.playerInput.GamePlay.Walk.canceled += (InputAction.CallbackContext ctx) =>
        {
            if (pc.isGround)
            {
                this.speed = this.runSpeed;
            }
        };
        #endregion
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("enemy"))
        {
            Charactor otherCha = other.GetComponent<Charactor>();
            Debug.Log("碰到了敌人");
            // 英雄受到敌人的伤害
            charactor?.TakeDamage(other.gameObject, otherCha.attackInstance.damage);
            Debug.Log("英雄的血量：" + charactor.Hp);
        }
    }


    void OnEnable()
    {
        this.playerInput.Enable();

    }

    void OnDisable()
    {
        this.playerInput.Disable();

    }

    // Update is called once per frame
    void Update()
    {
        this.inputDirection = this.playerInput.GamePlay.Move.ReadValue<Vector2>();
        InputAction jumpAction = this.playerInput.GamePlay.Jump;

    }

    void FixedUpdate()
    {
        if (this.isDead) return;

        if (this.inputDirection.x < 0)
        {
            this.transform.localScale = new Vector3(-1, 1, 1);
            lastInputDir = this.inputDirection;
        }
        else if (this.inputDirection.x > 0)
        {
            this.transform.localScale = new Vector3(1, 1, 1);
            lastInputDir = this.inputDirection;
        }

        if (!this.isCrouch && !isBounce)
            this.rb.velocity = new Vector2(this.inputDirection.x * speed * Time.deltaTime, this.rb.velocity.y);

        this.isCrouch = this.inputDirection.y < -0.2;
        this.playerAnimation.crouch(this.isCrouch);
        if (this.isCrouch)
        {
            cc2d.size = new Vector2(this.originSize.x, 1.68f);
            cc2d.offset = new Vector2(this.originOffset.x, 0.84f);
        }
        else
        {
            cc2d.size = this.originSize;
            cc2d.offset = this.originOffset;
        }

        if (isBounce)
        {
            this.rb.velocity = Vector2.zero;
            Vector2 force = this.bounceDirection * this.bounceForce;
            Debug.Log("弹开方向：" + force);

            this.rb.AddForce(force, ForceMode2D.Impulse);

            // this.isBounce = false;
        }
    }

    private void Jump(InputAction.CallbackContext obj)
    {
        if (this.isDead) return;
        if (isBounce) return;
        if (!pc.isGround) return;
        this.rb.AddForce(transform.up * this.jumpForce, ForceMode2D.Impulse);
        // this.playerAnimation.JumpAction();
    }

    /// <summary>
    /// 主角受到攻击弹开
    /// </summary>
    public void rebound(Transform attacker)
    {
        if (isDead) return;
        this.isBounce = true;
        this.bounceDirection = new Vector2((this.transform.position.x - attacker.position.x), 0).normalized;
    }

    public void playerDead(Transform attacker)
    {
        Debug.Log("主角死亡");
        this.isDead = true;
        this.rb.velocity = Vector2.zero;
        this.playerInput.Disable();
    }

    /// <summary>
    ///  攻击
    /// </summary>
    public void Attack(InputAction.CallbackContext attackObj)
    {
        if (this.isDead) return;
        if (!this.pc.isGround) return;

        Debug.Log("Attack---->>");
        // 处理攻击事件
        playerCombat.combatEvent();

    }

    /// <summary>
    /// 通过时间窗口触发攻击连招 
    /// </summary>
    private void _AtatckByTimeWindow()
    {

    }

    /// <summary>
    /// 重置攻击
    /// </summary>
    public void ResetAttack()
    {
        Debug.Log("reset attack");
        this.isAttack = false;
    }
}
