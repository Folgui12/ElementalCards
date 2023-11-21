using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrafoMA : IGrafoTDA
{
    public int[,] MAdy;
    public int[] nodosID;
    public int cantNodos;

    //public ConjuntoTA Vertices;

    public void InicializarGrafo(int size)
    {
        MAdy = new int[size, size];
        nodosID = new int[size];
        cantNodos = 0;
    }

    public void AgregarVertice(int v)
    {
        nodosID[cantNodos] = v;
        for (int i = 0; i <= cantNodos; i++)
        {
            MAdy[cantNodos, i] = 0;
            MAdy[i, cantNodos] = 0;
        }
        cantNodos++;
    }

    public void EliminarVertice(int v)
    {
        int ind = Vert2Indice(v);

        for (int k = 0; k < cantNodos; k++)
        {
            MAdy[k, ind] = MAdy[k, cantNodos - 1];
        }

        for (int k = 0; k < cantNodos; k++)
        {
            MAdy[ind, k] = MAdy[cantNodos - 1, k];
        }

        nodosID[ind] = nodosID[cantNodos - 1];
        cantNodos--;
    }

    public int Vert2Indice(int v)
    {
        int i = cantNodos - 1;
        while (i >= 0 && nodosID[i] != v)
        {
            i--;
        }

        return i;
    }

    /*public IConjuntoTDA VerticesEnConjunto()
    {
        Vertices = new ConjuntoTA();
        Vertices.cantNodos = cantNodos;
        Vertices.InicializarConjunto();
        for (int i = 0; i < cantNodos; i++)
        {
            Vertices.Agregar(nodosIDs[i]);
        }
        return Vertices;
    }*/

    public void AgregarArista(int v1, int v2, int peso)
    {
        int o = Vert2Indice(v1);
        int d = Vert2Indice(v2);
        MAdy[o, d] = peso;
    }

    public void EliminarArista(int v1, int v2)
    {
        int o = Vert2Indice(v1);
        int d = Vert2Indice(v2);
        MAdy[o, d] = 0;
    }

    public bool ExisteArista(int v1, int v2)
    {
        int o = Vert2Indice(v1);
        int d = Vert2Indice(v2);
        return MAdy[o, d] != 0;
    }

    public int PesoArista(int v1, int v2)
    {
        int o = Vert2Indice(v1);
        int d = Vert2Indice(v2);
        return MAdy[o, d];
    }

    
}
