using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using Unity.VisualScripting;
using UnityEngine;


/// <summary>
/// 野猪类
/// </summary>
public class Boar : Enemy
{

    public Vector2 startPoint;
    public Vector2 endPoint;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        State = UnitState.IDLE;
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        if (transform.position.x <= endPoint.x || transform.position.x >= startPoint.x)
        {
            // 强制拉回
            if (transform.position.x <= endPoint.x)
            {
                transform.position = new Vector2(endPoint.x, transform.position.y);
            }
            else
            {
                transform.position = new Vector2(startPoint.x, transform.position.y);
            }
            // 转向
            float targetX = -transform.localScale.x;
            transform.localScale = new Vector3(targetX, transform.localScale.y, transform.localScale.z);
        }
    }
}
