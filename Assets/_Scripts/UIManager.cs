using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI tesouroText, tesouroTextLose, rumText, rumTextLose, IlhaText, ilhaHistoriaVitoria;
    public Animator tesouroAnimLose, rumAnimLose;
    private ResourceManager rManager;
    private MissionManager mManager;
    private UpgradeManager uManager;
    private GameManager gManager;
    public GameObject textLosePrefab, textWinPrefab, panelIlha, panelBattle, panelGameOver, panelVictory, buttonAccept, buttonRefuse, buttonOk, panelPorto, panelInformation;

    public bool acceptMission = false;

    public AudioClip select;

    void Start()
    {
        rManager = GameObject.FindGameObjectWithTag("ResourceManager").GetComponent<ResourceManager>();
        mManager = GameObject.FindGameObjectWithTag("MissionManager").GetComponent<MissionManager>();
        uManager = GameObject.FindGameObjectWithTag("UpgradeManager").GetComponent<UpgradeManager>();
        gManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }


    void Update()
    {
        tesouroText.SetText(rManager.tesouro.ToString());
        rumText.SetText(rManager.rum.ToString());
    }

    public void ChamaPanelIlha()
    {
        if (!panelIlha.activeSelf)
        {
            panelIlha.SetActive(true);
            if (!mManager.ilhaAtiva.GetComponent<Ilha>().missao)
            {
                IlhaText.SetText(mManager.ilhaAtiva.GetComponent<Ilha>().textoIlhaNormal);
                buttonAccept.SetActive(true);
                buttonRefuse.SetActive(true);
                buttonOk.SetActive(false);
            }
            else
            {
                IlhaText.SetText(mManager.ilhaAtiva.GetComponent<Ilha>().textoIlhaMissao);
                buttonAccept.SetActive(false);
                buttonRefuse.SetActive(false);
                buttonOk.SetActive(true);
            }
            gManager.currentState = GameManager.GameState.Ilha;
        }
    }

    public void ChamaPanelPorto()
    {
        if (!panelPorto.activeSelf)
        {
            panelPorto.SetActive(true);           
            gManager.currentState = GameManager.GameState.Ilha;
        }
    }

    public void SpawnaTextoPerde(int quantidade, Transform posicao)
    {
        GameObject b = (GameObject)Instantiate(textLosePrefab, Vector3.zero, Quaternion.identity);
        b.GetComponent<TextMeshProUGUI>().SetText("-" + quantidade.ToString());
        b.transform.SetParent(posicao);
        b.transform.position = posicao.position;
        b.transform.localScale = Vector3.one;
    }

    public void SpawnaTextoGanha(int quantidade, Transform posicao)
    {
        GameObject b = (GameObject)Instantiate(textWinPrefab, Vector3.zero, Quaternion.identity);
        b.GetComponent<TextMeshProUGUI>().SetText("+" + quantidade.ToString());
        b.transform.SetParent(posicao);
        b.transform.position = posicao.position;
        b.transform.localScale = Vector3.one;
    }

    public void ButtonAccept()
    {
        mManager.ilhaAtiva.GetComponent<Ilha>().missao = true;
        IlhaText.SetText(mManager.ilhaAtiva.GetComponent<Ilha>().textoIlhaMissao);
        panelIlha.SetActive(false);
        panelBattle.SetActive(true);
        gManager.currentState = GameManager.GameState.Battle;
    }

    public void ButtonRefuse()
    {
        panelIlha.SetActive(false);
        gManager.currentState = GameManager.GameState.Gameplay;
    }

    public void ButtonOk()
    {
        panelIlha.SetActive(false);
        gManager.currentState = GameManager.GameState.Gameplay;
    }

    public void ButtonUpgrade()
    {
        if(rManager.tesouro >= uManager.custoTesouroUpgrade && rManager.rum >= uManager.custoRumUpgrade)
        {
            rManager.PerdeTesouro(uManager.custoTesouroUpgrade);
            rManager.PerdeRum(uManager.custoRumUpgrade);
            uManager.nivelBarco += 1;
        }
    }

    public void ButtonRum()
    {
        if (rManager.tesouro >= 1)
        {
            rManager.PerdeTesouro(1);
            rManager.GanhaRum(10);
        }
    }

    public void ButtonInformation()
    {
        if (rManager.rum >= 25)
        {
            rManager.PerdeRum(25);
            panelInformation.SetActive(true);
        }
    }

    public void ButtonInformationExit()
    {
        panelInformation.SetActive(false);
    }

    public void ButtonExit()
    {
        panelPorto.SetActive(false);
        gManager.currentState = GameManager.GameState.Gameplay;
    }

    public void ButtonExitVictory()
    {
        panelVictory.SetActive(false);
        gManager.currentState = GameManager.GameState.Gameplay;
    }

    public void ButtonGameOver()
    {
        panelGameOver.SetActive(false);
        gManager.currentState = GameManager.GameState.Gameplay;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ButtonExitGame()
    {
        SceneManager.LoadScene("Menu");
    }

    public void MouseOver()
    {
        SoundManager.instance.RandomizeSfx(select);
    }
}
