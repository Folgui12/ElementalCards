using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsManager
{
    public static EventsManager Instance
    {
        get
        {
            //Singletone que solamente se crea en memoria cuando se lo llama/necesita (Lazy Singletone)
            if (instance == null)
                instance = new EventsManager();
            return instance;
        }
    }

    private static EventsManager instance;

    //Simple Events
    private Dictionary<string, List<IListener>> simpleEvents = new();

    public void AddListener(string eventID, IListener p_listener)
    {
        if (simpleEvents.TryGetValue(eventID, out var listeners) && !listeners.Contains(p_listener))
            listeners.Add(p_listener);
    }

    public void RemoveListener(string eventID, IListener p_listener)
    {
        if (simpleEvents.TryGetValue(eventID, out var listeners) && listeners.Contains(p_listener))
            listeners.Remove(p_listener);
    }

    public void DispatchSimpleEvent(string eventID)
    {
        if (simpleEvents.TryGetValue(eventID, out var listeners))
        {
            foreach (var listener in listeners)
            {
                listener.OnEventDispatch();
            }
        }
    }

    public void RegisterEvents(string eventID)
    {
        if(!simpleEvents.ContainsKey(eventID))
            simpleEvents[eventID] = new List<IListener>();
    }
}
