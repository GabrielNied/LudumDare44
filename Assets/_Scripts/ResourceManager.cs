using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceManager : MonoBehaviour
{
    public int tesouro, rum, tripulacao, quantidadeRumPerde;

    public float tempoPerdeRum = 10, tempoPerdeRumReset = 10;

    private UIManager uiManager;
    private GameManager gManager;
    void Start()
    {
        uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
        gManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        if (gManager.currentState == GameManager.GameState.Gameplay)
        {
            tempoPerdeRum -= Time.deltaTime;
            if (tempoPerdeRum <= 0)
            {
                tempoPerdeRum = tempoPerdeRumReset;
                PerdeRum(quantidadeRumPerde);
            }
        }

        if(rum <= 0)
        {
            gManager.currentState = GameManager.GameState.Lose;
        }
    }
    

    public void PerdeTesouro(int quantidadeTesouroPerde)
    {
        tesouro -= quantidadeTesouroPerde;
        uiManager.SpawnaTextoPerde(quantidadeTesouroPerde, uiManager.tesouroText.transform);
    }

    public void PerdeRum(int quantidadeRumPerde)
    {
        rum -= quantidadeRumPerde;
        uiManager.SpawnaTextoPerde(quantidadeRumPerde, uiManager.rumText.transform);
    }

    public void GanhaTesouro(int quantidadeTesouroGanha)
    {
        tesouro += quantidadeTesouroGanha;
        uiManager.SpawnaTextoGanha(quantidadeTesouroGanha, uiManager.tesouroText.transform);
    }

    public void GanhaRum(int quantidadeRumGanha)
    {
        rum += quantidadeRumGanha;
        uiManager.SpawnaTextoGanha(quantidadeRumGanha, uiManager.rumText.transform);
    }
}
