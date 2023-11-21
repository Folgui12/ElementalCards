using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPila
{
    void Push(int x);

    void Pop();

    int Peek();

    bool EmptyStack();

    void StackInit();
}
