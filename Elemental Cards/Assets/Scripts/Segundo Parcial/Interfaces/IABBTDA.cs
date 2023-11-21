using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IABBTDA
{
    int Raiz();
    NodoArbol HijoIzq();
    NodoArbol HijoDer();
    bool ArbolVacio();
    void InicializarArbol();
    NodoArbol AgregarElem(int x, NodoArbol collider, GameObject info);
    void EliminarElem(NodoArbol nodo, int x);

}
