using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : Actor, IListener
{
    public int startManaCount = 3;
    public int CurrentMana;
    public bool CanPlay;
    public int ManaTotal => _totalMana;
    public SpellsQueue _spellsQueue;
    public ElementalPower ElementalShield => _elementalShield;
    public int ShieldedDamage => _shieldedDamage;

    [SerializeField] private int _totalMana;
    [SerializeField] private string ManaEventID;
    [SerializeField] private string ChangeDeckID;
    [SerializeField] private CardStack _cardStack;
    private ElementalPower _elementalShield;
    private int _shieldedDamage;
    public GameObject shield;
    private ChangeDeckListener _deckChanger;

    private void Awake()
    {
        CurrentMana = startManaCount;
        _showCurrentLife = GetComponentInChildren<Slider>();

        _showCurrentLife.maxValue = _stats.MaxLife;
        _showCurrentLife.value = _stats.MaxLife;
        _stats.CurrentLife = _stats.MaxLife;

        CanPlay = true;
    }

    private void Start()
    {
        _cardStack = GetComponent<CardStack>();
        _cardStack.StackInit();
        _cardStack.RellenarDeckPrimerasRondas();
        _cardStack.DrawFirstFiveCards();

        _spellsQueue = GetComponent<SpellsQueue>();
        _spellsQueue.InitQueue();

        EventsManager.Instance.AddListener(ManaEventID, this);

        _deckChanger = new ChangeDeckListener(this, ChangeDeckID, _cardStack);
        _deckChanger.InitListener();
    }

    private bool EmptyHand()
    {
        for(int i = 0; i < _cardStack.cardSlots.Length; i++)
        { 
            if(!_cardStack.cardSlots[i])
            {
                return false;
            }
        }
        return true;
    }

    public bool CheckPlayerVictory()
    {
        if (_cardStack.EmptyStack() && EmptyHand())
            return true;
        return false;
    }

    public bool CheckIfThereIsSpaceInHand()
    {
        for(int i = 0; i < _cardStack.availableCardSlots.Length; i++)
        {
            if (_cardStack.availableCardSlots[i])
            {
                return true;
            }
        }
        return false;
    }

    public void PlayCard(Card cardToPlay)
    {
        _spellsQueue.Enqueue(cardToPlay);
        _cardStack.availableCardSlots[cardToPlay.handIndex] = true;
    }

    public void EndTurn()
    {
        StartCoroutine(SpellCasting());
    }

    private IEnumerator SpellCasting()
    {
        while (!_spellsQueue.EmptyQueue())
        {
            Card spellCasted = _spellsQueue.Dequeue();

            if (spellCasted._stats.Damage > 0)
            {
                GameObject aux = Instantiate(spellCasted.spell, new Vector3(transform.position.x, transform.position.y + 4f, 0f), transform.rotation);

                while (aux.transform.position.x < 5f)
                {
                    yield return new WaitForSeconds(.025f);
                    aux.transform.position += new Vector3(.5f, -.1f, 0);
                }

                Destroy(aux);

                EnemyManager currentEnemy = FindObjectOfType<EnemyManager>();

                if(currentEnemy != null)
                    EventQueueManager.Instance.AddEvents(new CmdtakeDamage(currentEnemy, spellCasted._stats.Damage, spellCasted._stats.Element));
            }

            else
            {
                shield = Instantiate(spellCasted.spell, new Vector3(transform.position.x + 1f, transform.position.y + 1f, 0f), transform.rotation);

                _elementalShield = spellCasted._stats.Element;
                _shieldedDamage = spellCasted._stats.Protection;
            }
        }

        CanPlay = false;
        
    }

    public void DrawACard()
    {
        if(!_cardStack.EmptyStack())
            _cardStack.DrawNextCard();
        _elementalShield = null;
    }

    public void OnEventDispatch()
    {
        CurrentMana = startManaCount;
        GameManager.Instance.ChargeManaPoints();
    }

    public override void Die()
    {
        //Destroy(gameObject);
        ScreenManager.Instance.PlayerLose();
    }
}
