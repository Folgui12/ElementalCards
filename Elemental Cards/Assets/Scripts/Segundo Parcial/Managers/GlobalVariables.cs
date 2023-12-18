using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GlobalVariables : MonoBehaviour
{
    public static GlobalVariables Instance;
    public static bool check;
    public static int id;
    public static int confrontedEnemies = 0;
    public static int enemiesKilled = 0;
    public static string playerName;
    public static int deactivatedAreas = 0;
    public List<string> enemiesTags = new List<string>();

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

    }

    void Update()
    {
        
    }

    public void EraseEnemies()
    {
        if (confrontedEnemies > 0)
            for (int i = 0; i < enemiesTags.Count; i++)
            {
                for(int j = 0; j < enemiesTags.Count; j++)
                {
                    GameObject aux = GameObject.FindGameObjectWithTag(enemiesTags[j]);
                    if (enemiesTags[i] == aux.tag)
                    {
                        Destroy(aux);
                    }
                }
            }
    }

    public void Battle(int ID)
    {
        confrontedEnemies++;
        if(confrontedEnemies%2 == 0)
            deactivatedAreas++;
        id = ID;
        SceneManager.LoadScene(2);
    }

    public void Restore()
    {
        Debug.Log("Restoring");
        confrontedEnemies = 0;
        enemiesKilled = 0;
        deactivatedAreas = 0;
        enemiesTags.Clear();
    }
}
