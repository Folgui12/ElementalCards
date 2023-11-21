using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventQueueManager : MonoBehaviour
{
    static public EventQueueManager Instance;

    private void Awake()
    {
        if (Instance != null) Destroy(this);
        else Instance = this;
    }

    private Queue<ICommand> _events = new();

    public void AddEvents(ICommand newEvent) => _events.Enqueue(newEvent);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        while(_events.Count > 0)
        {
            _events.Dequeue().Do();
        }
    }
}
