using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodoArbol
{
    public int orden;

    public GameObject datoCollider;

    public NodoArbol izq = null;
    public NodoArbol der = null;

    public NodoArbol(int o, GameObject data)
    {
        orden = o;
        datoCollider = data;
    }
    
}
