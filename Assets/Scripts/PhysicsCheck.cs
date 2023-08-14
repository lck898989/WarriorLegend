using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    private Rigidbody2D rb;
    public bool isGround = false;

    public float checkDistance = 0.1f;

    public Vector2 checkOffset = Vector2.zero;
    public LayerMask layerMask;

    public PlayerController playerController;

    void Awake()
    {
        this.rb = this.GetComponent<Rigidbody2D>();
        playerController = GetComponent<PlayerController>();
        Debug.Log("PhysicsCheck Awake");
    }

    private void Update()
    {

    }

    void FixedUpdate()
    {
        bool isG = this.isGround;
        this.isGround = this.CheckGround();
        if (!isG && this.isGround)
        {
            Debug.Log("落地了");
            playerController.setNormalMaterial();

        }
    }

    private bool CheckGround()
    {
        Collider2D hit = Physics2D.OverlapCircle((Vector2)this.transform.position + checkOffset, checkDistance, this.layerMask);
        if (hit != null)
        {

            return true;
        }
        return false;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere((Vector2)this.transform.position + checkOffset, checkDistance);
    }
}
