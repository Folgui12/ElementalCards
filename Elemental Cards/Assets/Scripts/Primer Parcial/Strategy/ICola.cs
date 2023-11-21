using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICola
{
    void Enqueue(Card card);

    Card Dequeue();

    Card Peek();

    bool EmptyQueue();

    void InitQueue();
}
