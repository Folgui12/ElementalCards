using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConjuntoTA : IConjuntoTDA
{
    int[] a;
    int cant;
    public int cantNodos;

    public void Agregar(int x)
    {
        if (!this.Pertenece(x))
        {
            a[cant] = x;
            cant++;
        }
    }

    public bool ConjuntoVacio()
    {
        return cant == 0;
    }

    public int Elegir()
    {
        return a[cant - 1];
    }

    public void InicializarConjunto()
    {
        a = new int[cantNodos];
        cant = 0;
    }

    public bool Pertenece(int x)
    {
        int i = 0;
        while (i < cant && a[i] != x)
        {
            i++;
        }
        return (i < cant);
    }

    public void Sacar(int x)
    {
        int i = 0;
        while (i < cant && a[i] != x)
        {
            i++;
        }
        if (i < cant)
        {
            a[i] = a[cant - 1];
            cant--;
        }
    }
}
