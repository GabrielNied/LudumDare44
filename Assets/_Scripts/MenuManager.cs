using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    public AudioClip trilha, select;

    void Start()
    {
        if (!SoundManager.instance.musicSource.isPlaying)
        {
            SoundManager.instance.musicSource.clip = trilha;
            SoundManager.instance.musicSource.Play();
            SoundManager.instance.FadeIn(SoundManager.instance.musicSource, 0.5f, 0.25f);
        }
    }

    void Update()
    {
        
    }

    public void ButtonStart()
    {
        SoundManager.instance.FadeOut(SoundManager.instance.musicSource, 0f, 0.35f);
        SceneManager.LoadScene("Jogo");
    }
    public void ButtonCredits()
    {
        SceneManager.LoadScene("Credits");
    }
    public void ButtonExit()
    {
        Application.Quit();
    }
    public void ButtonExitCredits()
    {
        SceneManager.LoadScene("Menu");
    }

    public void MouseOver()
    {
        SoundManager.instance.RandomizeSfx(select);
    }
}
