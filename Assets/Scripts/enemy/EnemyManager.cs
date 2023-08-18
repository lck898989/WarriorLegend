using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    public Transform Player;

    private void Awake()
    {
        GameManager.Instance.enemyManager = this;
    }

}
