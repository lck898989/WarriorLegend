using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    public Animator animator;
    public Rigidbody2D rb;

    public PhysicsCheck pc;

    public PlayerController playerController;

    void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        pc = GetComponent<PhysicsCheck>();
        playerController = GetComponent<PlayerController>();

    }
    // Start is called before the first frame update
    void Update()
    {
        this.setAnimation();
    }

    public void setAnimation()
    {
        animator.SetFloat("velocityX", Mathf.Abs(rb.velocity.x));
        animator.SetFloat("velocityY", rb.velocity.y);
        animator.SetBool("isGround", pc.isGround);
        animator.SetBool("isDead", playerController.isDead);
        // 每帧检测是否是攻击状态
        animator.SetBool("isAttack", playerController.isAttack);
    }

    public void setTrigger(String trigger)
    {
        animator.SetTrigger(trigger);
    }

    public void JumpAction()
    {
        // animator.SetBool("isGround", pc.isGround);
    }

    public void hited()
    {
        animator.SetTrigger("hit");
    }

    public void crouch(bool tag)
    {
        animator.SetBool("isCrouch", tag);
    }
}
