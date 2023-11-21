using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GrafoMaker : MonoBehaviour
{
    [SerializeField] Dictionary<int, Nodo> nodos;
    private int cantNodos;
    GrafoMA miGrafo;
    private bool startProgram = false;
    private int nextNode = 0;
    private AlgDijkstra dij = new AlgDijkstra();

    // Start is called before the first frame update
    void Start()
    {
        startProgram = true;

        nodos = new Dictionary<int, Nodo>();

        cantNodos = 0;
        miGrafo = new GrafoMA();

        while(transform.childCount > cantNodos)
        {
            nodos.Add(transform.GetChild(cantNodos).gameObject.GetComponent<Nodo>().ID, transform.GetChild(cantNodos).gameObject.GetComponent<Nodo>());
            cantNodos++;
        }

        miGrafo.InicializarGrafo(cantNodos);

        List<int> keys = new List<int>(nodos.Keys);

        for(int i = 0; i < cantNodos; i++)
        {
            miGrafo.AgregarVertice(keys[i]);
        }

        GenerarAristas();

        //REVISAR MATRIZ DE ADYACENCIA
        /*for(int i = 0; i < miGrafo.cantNodos; i++)
        {
            Debug.Log(miGrafo.nodosID[i]);
            for(int h = 0; h < miGrafo.cantNodos; h++)
            {
                Debug.Log("Nodos: " + miGrafo.nodosID[h]);
                Debug.Log("Peso: " + miGrafo.MAdy[i, h]);
            }
        }*/

        dij.Dijkstra(miGrafo, miGrafo.nodosID[0]);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void GenerarAristas()
    {
        for(int i = 0; i < miGrafo.cantNodos; i++)
        {
            if(i == miGrafo.cantNodos-1)
            {
                miGrafo.AgregarArista(miGrafo.nodosID[i], miGrafo.nodosID[0], i+2);
            }
            else
                miGrafo.AgregarArista(miGrafo.nodosID[i], miGrafo.nodosID[i+1], i+2);
        }
        miGrafo.AgregarArista(miGrafo.nodosID[4], miGrafo.nodosID[2], 6);
        miGrafo.AgregarArista(miGrafo.nodosID[1], miGrafo.nodosID[3], 10);
        miGrafo.AgregarArista(miGrafo.nodosID[4], miGrafo.nodosID[6], 13);
    }

    public Nodo CaminoARecorrer()
    {
        for(int i = 0; i < cantNodos; i++)
        {
            for(int h = 0; h < cantNodos; h++)
            {
                if( nextNode < cantNodos && nodos.ContainsKey(dij.nodos[nextNode]))
                {
                    nextNode++;

                    if(nextNode > cantNodos-1)
                        nextNode = 0;
                
                    return nodos[dij.nodos[nextNode]];
                }
                    
            }
        }
        return null;
    }

    void OnDrawGizmos()
    {
        if(startProgram)
        {
            Gizmos.color = Color.green;
        
            for(int i = 0; i < miGrafo.cantNodos; i++)
            {
                for(int h = 0; h < miGrafo.cantNodos; h++)
                {
                   if(miGrafo.ExisteArista(i+1, h+1))
                   { 
                        Gizmos.DrawLine(nodos[miGrafo.nodosID[i]].transform.position, nodos[miGrafo.nodosID[h]].transform.position);
                   } 
                }
                
            }
        }
    }
}
