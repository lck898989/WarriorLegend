using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{

    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameManager();
            }
            return instance;
        }
    }
    public LegendGame legendGame;
    public EnemyManager enemyManager;
}
