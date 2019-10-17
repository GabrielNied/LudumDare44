using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum GameState { Ilha, Gameplay, Battle, Victory, Lose }
    public GameState currentState = GameState.Gameplay;

    public bool playerPodeAndar = false, pause = false;

    private UIManager uiManager;

    public AudioClip trilha;

    public GameObject pauseText, exitGameButton;

    void Start()
    {
        uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();

        SoundManager.instance.musicSource.volume = 0;
        SoundManager.instance.musicSource.clip = trilha;
        SoundManager.instance.musicSource.Play();
        SoundManager.instance.FadeIn(SoundManager.instance.musicSource, 0.5f, 0.1f);
    }

    void Update()
    {
        if (currentState == GameState.Gameplay)
        {
            playerPodeAndar = true;
        }
        else
        {
            playerPodeAndar = false;
        }
        if (currentState == GameState.Ilha)
        {

        }
        if (currentState == GameState.Battle)
        {

        }
        if (currentState == GameState.Victory)
        {
            uiManager.panelBattle.SetActive(false);
            currentState = GameState.Gameplay;
        }
        if (currentState == GameState.Lose)
        {
            uiManager.panelGameOver.SetActive(true);
        }

        if (Input.GetButtonDown("Cancel") && !pause)
        {
            Time.timeScale = 0;
            pause = true;
            pauseText.SetActive(true);
            exitGameButton.SetActive(true);
        }
        else if (Input.GetButtonDown("Cancel") && pause)
        {
            Time.timeScale = 1;
            pause = false;
            pauseText.SetActive(false);
            exitGameButton.SetActive(false);
        }
    }

    public void VictoryHist(string text)
    {
        uiManager.panelBattle.SetActive(false);
        uiManager.panelVictory.SetActive(true);
        uiManager.ilhaHistoriaVitoria.SetText(text);
    }
}
