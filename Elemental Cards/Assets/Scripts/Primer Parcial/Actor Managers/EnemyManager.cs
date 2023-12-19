using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : Actor, IListener, IProduct
{
    public ElementalPower Weakness;
    public ElementalPower Resistance;
    public GameObject Spell;
    private float _extraDamageTakenCauseWeakness = 2;
    [SerializeField] private string _eventID;
    [SerializeField] private int _damage;
    private PlayerManager _playerRef; 
    private CmdtakeDamage _enemyAttack;

    private void Start()
    {
        EventsManager.Instance.AddListener(_eventID, this);
        Debug.Log("Listener Added");

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
            _stats.CurrentLife -= damage * _extraDamageTakenCauseWeakness;
        }
        else
        {
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
        StartCoroutine(SpellCasting());
    }

    private IEnumerator SpellCasting()
    {
        Debug.Log("HOLA");
        GameObject aux = Instantiate(Spell, new Vector3(transform.position.x, transform.position.y + 4f, 0f), transform.rotation);

        aux.transform.eulerAngles = new Vector3(0, 0, -110);

        yield return new WaitForSeconds(.2f);

        while (aux.transform.position.x > -5f)
        {
            yield return new WaitForSeconds(.025f);
            aux.transform.position -= new Vector3(.5f, .1f, 0);
        }

        Destroy(aux);

        for (int i = 0; i < GameManager.Instance.ElementalPowers.Count; i++)
        {
            if (_stats.Power == GameManager.Instance.ElementalPowers[i])
            {
                Debug.Log($"Enemy threw {_stats.Power} ball");
                if (_playerRef.ElementalShield != _stats.Power)
                {
                    EventQueueManager.Instance.AddEvents(_enemyAttack);
                    Destroy(_playerRef.shield);
                }
                else if (_playerRef.ElementalShield == _stats.Power)
                {
                    _enemyAttack.UnDo();
                    Destroy(_playerRef.shield);
                }
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
