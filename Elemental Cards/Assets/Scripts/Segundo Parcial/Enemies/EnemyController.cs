using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private GrafoMaker grafo;
    public Nodo nuevoCamino;
    //public GlobalVariables globalVariables;
    public int ID;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        grafo = transform.parent.GetChild(GetComponentInParent<Transform>().GetSiblingIndex()+1).GetComponent<GrafoMaker>();
    }

    // Update is called once per frame
    void Update()
    {

        if(transform.position == nuevoCamino.transform.position)
        {
            nuevoCamino = grafo.CaminoARecorrer();
        }
        else
            transform.position = Vector2.MoveTowards(transform.position, nuevoCamino.transform.position, speed*Time.deltaTime);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 3)
        {
            GlobalVariables.Instance.enemiesTags.Add(this.GetComponentInParent<Transform>().gameObject.tag);
            GlobalVariables.Instance.Battle(ID);
        }
        
    }
}
