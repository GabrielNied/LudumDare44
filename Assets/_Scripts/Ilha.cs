using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ilha : MonoBehaviour
{
    public string textoIlhaNormal, textoIlhaMissao, textoHistoriaVitoria;

    public bool parouIlha = false, missao = false, chamaHistoria = false;

    public int dificuldade, recompensa, quantidadeEnemyIlha;

    private UIManager uiManager;

    public Sprite spriteInimigo, spriteIlha;

    void Start()
    {
        uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
    }

    void Update()
    {
        if (parouIlha)
        {
            if (Input.GetButtonDown("Interact"))
            {
                uiManager.ChamaPanelIlha();
            }
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.GetComponent<Rigidbody2D>().velocity.magnitude <= 0.1f)
            {
                parouIlha = true;
            }
            else
            {
                parouIlha = false;
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        parouIlha = false;
    }
}
