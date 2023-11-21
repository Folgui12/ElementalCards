using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardStack : MonoBehaviour, IPila
{
    public int MaxAmountOffCardsInDeck;

    public bool[] availableCardSlots;
    public Transform[] cardSlots;

    private CardFactory _cardFactory;
    [SerializeField] int[] deck;
    int index = 0;

    public void StackInit()
    {
        deck = new int[MaxAmountOffCardsInDeck];
        index = 0;
        _cardFactory = new CardFactory(GameManager.Instance.availableCards[0]);
    }

    public void Push(int x)
    {
        deck[index] = x;
        index++;
    }

    public void Pop()
    {
        index--;
    }

    public bool EmptyStack() => (index == 0);

    public int Peek() => deck[index-1];

    public void DrawFirstFiveCards()
    {
        for (int i = 0; i < availableCardSlots.Length; i++)
        {
            if (availableCardSlots[i] == true)
            {
                int nextCardToDraw = Peek();
                Card newCard = _cardFactory.CreateProduct(nextCardToDraw);
                newCard.handIndex = i;
                newCard.transform.position = cardSlots[i].position;
                availableCardSlots[i] = false;
                Pop();
            }
        }
    }

    public void RellenarDeckPrimerasRondas()
    {
        for (int i = 0; i < 10; i++)
            Push(Random.Range(0, 4));
    }

    public void CambiarADeckAvanzado()
    {
        Debug.Log("Cambiando a DeckAvanzado");
        while (!EmptyStack())
        {
            Pop();
        }

        for (int i = 0; i < MaxAmountOffCardsInDeck; i++)
            Push(Random.Range(0, 12));
    }

    public void DrawNextCard()
    {
        int nextCardToDraw = Peek();
        Pop();
        Card newCard = _cardFactory.CreateProduct(nextCardToDraw);
        for (int i = 0; i < availableCardSlots.Length; i++)
        {
            if (availableCardSlots[i] == true)
            {
                newCard.transform.position = cardSlots[i].position;
                newCard.handIndex = i;
                availableCardSlots[i] = false;
                return;
            }
        }
    }
}
