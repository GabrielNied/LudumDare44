using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Porto : MonoBehaviour
{
    public bool parouPorto = false;

    private UIManager uiManager;

    public TextMeshProUGUI textInfo, titleText;

    public string textoInfo, textTitle;

    void Start()
    {
        uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
    }

    void Update()
    {
        if (parouPorto)
        {
            if (Input.GetButtonDown("Interact"))
            {
                uiManager.ChamaPanelPorto();
                titleText.SetText(textTitle);
                textInfo.SetText(textoInfo);
            }
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.GetComponent<Rigidbody2D>().velocity.magnitude <= 0.1f)
            {
                parouPorto = true;
            }
            else
            {
                parouPorto = false;
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        parouPorto = false;
    }
}
