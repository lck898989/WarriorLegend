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
    }

    public override void Move()
    {
        base.Move();
    }
}
