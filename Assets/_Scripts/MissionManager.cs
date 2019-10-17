using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionManager : MonoBehaviour
{
    public GameObject ilhaAtiva, ilha;

    private ResourceManager rManager;
    private GameManager gManager;

    public int quantidadeEnemy, quantidadeEnemyMorto;

    public bool pegaInfo = true;

    void Start()
    {
        rManager = GameObject.FindGameObjectWithTag("ResourceManager").GetComponent<ResourceManager>();
        gManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gManager.currentState == GameManager.GameState.Ilha)
        {
            if (ilhaAtiva != null)
            {
                ilha.GetComponent<Image>().sprite = ilhaAtiva.GetComponent<Ilha>().spriteIlha;
                if (pegaInfo)
                {
                    infosCombate(ilhaAtiva.GetComponent<Ilha>().dificuldade, ilhaAtiva.GetComponent<Ilha>().quantidadeEnemyIlha, ilhaAtiva.GetComponent<Ilha>().spriteInimigo);
                    quantidadeEnemy = ilhaAtiva.GetComponent<Ilha>().quantidadeEnemyIlha;
                    pegaInfo = false;
                }
            }
        }
        else
        {
            pegaInfo = true;
        }

        if (gManager.currentState == GameManager.GameState.Battle)
        {
            if (quantidadeEnemyMorto >= quantidadeEnemy)
            {
                Victory();
            }
        }
    }

    public void infosCombate(int dificuldade, int quantidade, Sprite sprite)
    {
        ilha.GetComponent<SpawnEnemy>().dificuldade = dificuldade;
        ilha.GetComponent<SpawnEnemy>().quantidadeEnemy = quantidade;
        ilha.GetComponent<SpawnEnemy>().enemySprite = sprite;
    }

    public void Victory()
    {
        quantidadeEnemyMorto = 0;
        pegaInfo = true;
        if (ilhaAtiva.GetComponent<Ilha>().chamaHistoria)
        {
            gManager.VictoryHist(ilhaAtiva.GetComponent<Ilha>().textoHistoriaVitoria);
        }
        else
        {
            gManager.currentState = GameManager.GameState.Victory;
        }
        rManager.GanhaTesouro(ilhaAtiva.GetComponent<Ilha>().recompensa);
    }

    public void RecebeDano()
    {
        if (rManager.tesouro >= 1)
        {
            rManager.PerdeTesouro(1);
        }
        else
        {
            rManager.PerdeRum(1);
        }
    }
}
