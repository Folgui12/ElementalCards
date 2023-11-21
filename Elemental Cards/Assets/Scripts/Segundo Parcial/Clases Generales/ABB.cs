using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ABB : IABBTDA
{
    public NodoArbol raiz;

    public NodoArbol AgregarElem(int x, NodoArbol collider, GameObject info)
    {
        NodoArbol temp = null;

        if(collider == null)
        {
            temp = new NodoArbol(x, info);

            return temp;
        }
        else if(collider.orden >= x)
            collider.izq = AgregarElem(x, collider.izq, info);
        else if(collider.orden < x)
            collider.der = AgregarElem(x, collider.der, info);

        return collider;

    }

    public bool ArbolVacio()
    {
        return raiz == null;
    }

    public void EliminarElem(NodoArbol nodo, int x)
    {
        if(nodo != null)
        {
            if(nodo.orden == x && (nodo.izq==null) && (nodo.der==null))
            {
                nodo = null;
            }
            else if(nodo.orden == x && nodo.izq != null)
            {
                nodo.orden = this.mayor(nodo.izq);
                EliminarElem(nodo.izq, nodo.orden);
            }
            else if(nodo.orden == x && nodo.izq == null)
            {
                nodo.orden = this.menor(nodo.der);
                EliminarElem(nodo.der, nodo.orden);
            }
            else if(nodo.orden < x)
            {
                EliminarElem(nodo.der, x);
            }
            else
            {
                EliminarElem(nodo.izq, x);
            }
        }
    }

    public int mayor(NodoArbol a)
    {
        if(a.der == null)
        {
            return a.orden;
        }
        else
            return mayor(a.der);
    }

    public int menor(NodoArbol a)
    {
        if (a.izq == null)
        {
            return a.orden;
        }
        else
        {
            return menor(a.izq);
        }
    }

    public NodoArbol HijoDer()
    {
        return raiz.der;
    }

    public NodoArbol HijoIzq()
    {
        return raiz.izq;
    }

    public void InicializarArbol()
    {
        raiz = null;
    }

    public int Raiz()
    {
        return raiz.orden;
    }

    public void level_Order(NodoArbol nodo, int nivel)
    {
        Queue<NodoArbol> q = new Queue<NodoArbol>();

        int contDePiso = 0;

        q.Enqueue(nodo);

        //Debug.Log(nivel);

        while (q.Count > 0 && nodo != null)
        {
            nodo = q.Dequeue();

            if(contDePiso <= nivel)
            {
                nodo.datoCollider.SetActive(false);
            }

            if(nodo.izq != null) 
            { 
                q.Enqueue(nodo.izq);
                contDePiso++;
            }

            if (nodo.der != null) 
            {
                q.Enqueue(nodo.der);
            }   
        }
    }

    public void inOrder(NodoArbol a)
    {
        if (a != null)
        {
            inOrder(a.izq);
            Debug.Log(a.datoCollider.ToString());
            inOrder(a.der);
        }
    }

    public void postOrder(NodoArbol a)
    {
        if (a != null)
        {
            postOrder(a.izq);
            postOrder(a.der);
            Debug.Log(a.datoCollider);
        }
    }
}
