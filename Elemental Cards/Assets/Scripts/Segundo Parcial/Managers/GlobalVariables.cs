using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalVariables : MonoBehaviour
{
    public static GlobalVariables Instance;
    public static bool check;
    public static int id;
    public static int confrontedEnemies = 0;
    public static int deactivatedAreas = 0;
    public List<EnemyController> currentEnemies = new List<EnemyController>();
    public bool[] activeEnemies = {true, true, true, true, true, true , true, true, true, true};

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        EraseEnemies();
    }

    void Update()
    {
        
    }

    public void EraseEnemies()
    {
        if (confrontedEnemies > 0)
            for (int i = 0; i < activeEnemies.Length; i++)
            {
                if (!activeEnemies[i])
                {
                    Debug.Log("Enemigo Borrado");  
                    Destroy(currentEnemies[i].gameObject);
                    break;
                }
            }
    }

    public void Battle(int ID)
    {
        activeEnemies[confrontedEnemies] = false;
        confrontedEnemies++;
        if(confrontedEnemies%2 == 0)
            deactivatedAreas++;
        id = ID;
        SceneManager.LoadScene(2);
    }
}
