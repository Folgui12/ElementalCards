using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeDeckListener : IListener
{
    private PlayerManager _playerManager;
    private string _changeDeckEventID;
    private CardStack _currentDeck;

    public ChangeDeckListener(PlayerManager playerManager, string changeDeckEventID, CardStack currentDeck)
    {
        _playerManager = playerManager;
        _changeDeckEventID = changeDeckEventID;
        _currentDeck = currentDeck;
    }

    public void InitListener()
    {
        EventsManager.Instance.AddListener(_changeDeckEventID, this);
    }

    public void OnEventDispatch()
    {
        _currentDeck.CambiarADeckAvanzado();
    }
}
