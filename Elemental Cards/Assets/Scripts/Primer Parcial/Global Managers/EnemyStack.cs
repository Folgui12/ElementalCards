using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStack : MonoBehaviour, IPila
{
    public int MaxAmountOffEnemies;
    public Transform spawnPoint;

    private EnemyFactory _enemyFactory;
    int[] enemyList;
    private int index = 0;

    public void StackInit()
    {
        enemyList = new int[GlobalVariables.confrontedEnemies];
        index = 0;
        _enemyFactory = new EnemyFactory(GameManager.Instance.availableEnemies[0]);
        EnemyListInit();
    }

    public void Push(int x)
    {
        //Debug.Log(index);
        enemyList[index] = x;
        index++;
    }

    public void Pop() => index--;

    public bool EmptyStack() => (index == 0);

    public int Peek() => enemyList[index - 1];

    public void EnemyListInit()
    {
        for (int i = 0; i < GlobalVariables.confrontedEnemies; i++)
        {
            if(i < 7)
            {
                if (i == 6)
                {
                    Push(GlobalVariables.id);
                }
                else
                    Push(Random.Range(0, 4));
            }
            
            //Debug.Log(i);
        }
            
    }

    public void SpawnNextEnemy()
    {
        Debug.Log(index);
        _enemyFactory.CreateProduct(Peek()).transform.position = spawnPoint.position;
        Pop();
    }
}
