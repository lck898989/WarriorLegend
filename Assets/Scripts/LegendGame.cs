using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegendGame : MonoBehaviour
{
    public EnemyManager enemyManager;
    public Transform Player;

    private void Awake()
    {
        GameManager.Instance.legendGame = this;
    }
}
