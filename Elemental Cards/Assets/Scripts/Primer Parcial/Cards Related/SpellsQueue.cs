using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellsQueue : MonoBehaviour, ICola
{
    private Card[] spellsToCast;
    private int index;
    private int MaxSpellsToCast = 3;
    [SerializeField] private Sprite[] _elementsImages; // Tierra - Fuego - Aire - Agua
    [SerializeField] private Image[] _showCardInQueue;

    public void InitQueue()
    {
        spellsToCast = new Card[MaxSpellsToCast];
        index = 0;
    }

    public void Enqueue(Card card)
    {
        if (index > 0)
        {
            for (int i = index; i > 0; i--)
            {
                spellsToCast[i] = spellsToCast[i - 1];
                _showCardInQueue[i].sprite = spellsToCast[i - 1].ElementImage.sprite;
            }
        }
        spellsToCast[0] = card;
        _showCardInQueue[0].sprite = card.ElementImage.sprite;
        index++;
    }

    public bool EmptyQueue() => (index == 0);

    public Card Dequeue()
    {
        for (int i = 0; i < _showCardInQueue.Length; i++)
        {
            _showCardInQueue[i].sprite = null;
        }

        Card aux = Peek();

        spellsToCast[index - 1] = null;

        index--;

        return aux;
    }

    public Card Peek() => spellsToCast[index - 1];

    public bool FullQueue() => index == MaxSpellsToCast;
}
