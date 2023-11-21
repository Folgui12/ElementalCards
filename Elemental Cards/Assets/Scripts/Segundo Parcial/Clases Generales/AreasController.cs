using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreasController : MonoBehaviour
{
    public Dictionary<int, GameObject> zonas;

    public List<GameObject> listaZonas = new List<GameObject>();

    public ABB arbolDeZonas;

    public NodoArbol raiz;

    // Start is called before the first frame update
    void Start()
    {
        arbolDeZonas = new ABB();
        arbolDeZonas.InicializarArbol();
        zonas = new Dictionary<int, GameObject>();
        InicializarDiccionarioDeZonas();
        InicializarArbol();
        arbolDeZonas.level_Order(raiz, GlobalVariables.deactivatedAreas);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InicializarDiccionarioDeZonas()
    {
        for(int i = 0; i < listaZonas.Count; i++)
        {
            zonas.Add(i, listaZonas[i]);
        }
    }

    private void InicializarArbol()
    {
        int aux = 0;

        //Debug.Log(transform.childCount);
        for(int i = 0; i < transform.childCount; i++)
        {
            if(i%2==0)
            {
                aux++;
            }
            if(i == 0)
            {
                raiz = arbolDeZonas.AgregarElem(aux, null, zonas[i]);
            }
            else
                arbolDeZonas.AgregarElem(aux, raiz, zonas[i]);
        }
    }

    private void VerificarDiccionario()
    {
        foreach (var go in zonas)
        {
            Debug.Log(go);
        }
    }
}
