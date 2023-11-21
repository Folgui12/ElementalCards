using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : Actor, IListener, IProduct
{
    public ElementalPower Weakness;
    public ElementalPower Resistance;
    private float _extraDamageTakenCauseWeakness = 2;
    [SerializeField] private string _eventID;
    [SerializeField] private int _damage;
    private PlayerManager _playerRef; 
    private CmdtakeDamage _enemyAttack;

    private void Start()
    {
        EventsManager.Instance.AddListener(_eventID, this);
        _showCurrentLife = GetComponentInChildren<Slider>();

        _showCurrentLife.maxValue = _stats.MaxLife;
        _showCurrentLife.value = _stats.MaxLife;

        _stats.CurrentLife = _stats.MaxLife;

        _playerRef = FindObjectOfType<PlayerManager>();

        _enemyAttack = new CmdtakeDamage(_playerRef, _damage);
    }

    private void Update()
    {
    
    }

    public override void TakeDamage(float damage, ElementalPower element)
    {
        if(Weakness == element)
        {
            Debug.Log("Weak!");
            _stats.CurrentLife -= damage * _extraDamageTakenCauseWeakness;
        }
        else
        {
            Debug.Log("Resist!");
            _stats.CurrentLife -= damage;
        }

        UpdateLifeBar();

        if (_stats.CurrentLife <= 0)
        {
            Die();
            EventsManager.Instance.RemoveListener(_eventID, this);
            EventsManager.Instance.DispatchSimpleEvent("GainManaEvent");
            GameManager.Instance.NextEnemy();
        }
            
    }

    private void CastSpellToPlayer()
    {
        for(int i = 0; i < GameManager.Instance.ElementalPowers.Count; i++)
        {
            if(_stats.Power == GameManager.Instance.ElementalPowers[i])
            {
                Debug.Log($"Enemy threw {_stats.Power} ball");
                if (_playerRef.ElementalShield != _stats.Power)
                {
                    EventQueueManager.Instance.AddEvents(_enemyAttack);
                }
                else if(_playerRef.ElementalShield == _stats.Power)
                {
                    _enemyAttack.UnDo();
                }
                return; 
            }
        }
    }

    public void OnEventDispatch()
    {
        CastSpellToPlayer();
        _playerRef.CanPlay = true; 
    }

    public IProduct Clone(int index)
    {
        return Instantiate(GameManager.Instance.availableEnemies[index]);
    }
}
