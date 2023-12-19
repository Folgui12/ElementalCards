using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public List<GameObject> TotalMana = new();
    public List<ElementalPower> ElementalPowers = new();
    public List<Card> availableCards = new();
    public List<EnemyManager> availableEnemies = new();
    public string ManaPlayerEventID;
    public int EnemyKilled => _enemyKilled;

    [SerializeField] private PlayerManager playerRef;
    [SerializeField] private string eventID;
    [SerializeField] private string ChangePlayerDeckEventID;
    [SerializeField] private GameObject _endTurnButton;
    private EnemyStack _enemies;
    private int playerCurrentMana;
    private int _enemyKilled = 0;

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

        EventsManager.Instance.RegisterEvents(eventID);
        EventsManager.Instance.RegisterEvents(ManaPlayerEventID);
        EventsManager.Instance.RegisterEvents(ChangePlayerDeckEventID);

    }

    private void Start()
    {
        _enemies = GetComponent<EnemyStack>();

        _enemies.StackInit();

        foreach (GameObject mana in TotalMana)
        {
            mana.GetComponent<SpriteRenderer>().color = Color.black;
        }

        _enemies.SpawnNextEnemy();

        playerCurrentMana = playerRef.CurrentMana;

        ChargeManaPoints();
        
    }

    private void Update()
    {
        if(playerRef.CanPlay)
        {
            _endTurnButton.SetActive(true);
        }
        else
            _endTurnButton.SetActive(false);
    }

    public void ChargeManaPoints()
    {
        playerRef.CurrentMana = playerCurrentMana;
        for (int i = 0; i < playerCurrentMana; i++)
        {
            TotalMana[i].GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    public void UpdateMana(int cost)
    {
        int index = playerRef.CurrentMana - 1;
        while(cost > 0)
        {
            TotalMana[index].GetComponent<SpriteRenderer>().color = Color.black;
            cost--;
            index--;
        }
    }

    public void EnemyTurn()
    {
        StartCoroutine(TimeBetweenTurns());
    }

    public void NextEnemy()
    {
        StartCoroutine(TimeBetweenEnemies());
    }

    private IEnumerator TimeBetweenTurns()
    {
        yield return new WaitForSeconds(2f);

        EventsManager.Instance.DispatchSimpleEvent(eventID);

        ChargeManaPoints();

        if (playerRef.CheckIfThereIsSpaceInHand())
        {
            playerRef.DrawACard();
            if (_enemyKilled >= 2 && playerRef.CheckIfThereIsSpaceInHand())
            {
                playerRef.DrawACard();
            }
        }
        else
            Debug.Log("Can't draw another card, hand's full");

        StopCoroutine(TimeBetweenTurns());
    }

    private IEnumerator TimeBetweenEnemies()
    {
        yield return new WaitForSeconds(2f);

        playerRef.CanPlay = true;

        _enemyKilled++;
        GlobalVariables.enemiesKilled++;

        if (_enemies.EmptyStack())
            ScreenManager.Instance.ReturnToMap();
        else
        {
            _enemies.SpawnNextEnemy();
        }
            
            

        if(playerRef.startManaCount < playerRef.ManaTotal)
            playerRef.startManaCount++;

        playerCurrentMana = playerRef.startManaCount;

        ChargeManaPoints();

        if (_enemyKilled == 2)
            EventsManager.Instance.DispatchSimpleEvent(ChangePlayerDeckEventID);

        StopCoroutine(TimeBetweenEnemies());
            
    }
    
}
