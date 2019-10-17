using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private MissionManager mManager;
    private GameObject alvo;

    public float velocidade;

    void Start()
    {
        mManager = GameObject.FindGameObjectWithTag("MissionManager").GetComponent<MissionManager>();
        alvo = GameObject.FindGameObjectWithTag("Barco");
    }


    void Update()
    {
        float step = velocidade * Time.deltaTime;

        transform.position = Vector2.MoveTowards(transform.position, alvo.transform.position, step);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       if(collision.gameObject.tag == "Tiro")
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
            mManager.quantidadeEnemyMorto += 1;
        }

        if (collision.gameObject.tag == "Barco")
        {
            mManager.RecebeDano();
            Destroy(this.gameObject);
            mManager.quantidadeEnemyMorto += 1;
        }
    }
}
