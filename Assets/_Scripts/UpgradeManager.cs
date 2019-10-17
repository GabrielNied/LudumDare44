using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeManager : MonoBehaviour
{
    public int nivelBarco = 1, custoTesouroUpgrade, custoRumUpgrade;
    public Sprite nivelUm, nivelDois, nivelTres;

    public GameObject player, barcoUpgrade, tesouoInterface, rumInterface;
    public TextMeshProUGUI custoTesouroText, custoRumText, buttonUpgradeText;

    private ResourceManager rManager;

    void Start()
    {
        rManager = GameObject.FindGameObjectWithTag("ResourceManager").GetComponent<ResourceManager>();
    }

    void Update()
    {
        if(nivelBarco == 1)
        {
            player.GetComponent<SpriteRenderer>().sprite = nivelUm;
            barcoUpgrade.GetComponent<Image>().sprite = nivelDois;

            custoTesouroUpgrade = 10;
            custoRumUpgrade = 25;

            custoTesouroText.SetText(custoTesouroUpgrade.ToString());
            custoRumText.SetText(custoRumUpgrade.ToString());
        }
        if (nivelBarco == 2)
        {
            barcoUpgrade.GetComponent<Image>().sprite = nivelTres;
            player.GetComponent<SpriteRenderer>().sprite = nivelDois;

            custoTesouroUpgrade = 20;
            custoRumUpgrade = 50;

            custoTesouroText.SetText(custoTesouroUpgrade.ToString());
            custoRumText.SetText(custoRumUpgrade.ToString());
        }
        if (nivelBarco == 3)
        {
            tesouoInterface.SetActive(false);
            rumInterface.SetActive(false);
            buttonUpgradeText.SetText("Maxed Out");
            barcoUpgrade.GetComponent<Image>().sprite = nivelTres;
            player.GetComponent<SpriteRenderer>().sprite = nivelTres;
        }
    }    
}
