using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarcoAttack : MonoBehaviour
{
    public Image reload;
    public GameObject pai, player, bulletPrefab, clickText;
    public float cooldown = 1f, bulletSpeed = 4f, cooldownTime;

    private UpgradeManager uManager;
    private GameManager gManager;

    public Sprite barcoBatalhaUm, barcoBatalhaDois, barcoBatalhaTres;

    public AudioClip tiro;

    private void OnEnable()
    {
        clickText.SetActive(true);
        uManager = GameObject.FindGameObjectWithTag("UpgradeManager").GetComponent<UpgradeManager>();
        gManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        if (uManager.nivelBarco == 1)
        {
            this.GetComponent<Image>().sprite = barcoBatalhaUm;
            bulletSpeed = 5;
            cooldownTime = 2f;
        }
        if (uManager.nivelBarco == 2)
        {
            this.GetComponent<Image>().sprite = barcoBatalhaDois;
            bulletSpeed = 6;
            cooldownTime = 1.5f;
        }
        if (uManager.nivelBarco == 3)
        {
            this.GetComponent<Image>().sprite = barcoBatalhaTres;
            bulletSpeed = 7;
            cooldownTime = 1f;
        }
    }

    void Update()
    {     
        if (gManager.currentState == GameManager.GameState.Battle)
        {            
            if (cooldown < 1f)
            {
                cooldown += Time.deltaTime / cooldownTime;
                reload.fillAmount = cooldown;
            }
            else
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    clickText.SetActive(false);
                    Tiro();
                    cooldown = 0f;
                    SoundManager.instance.RandomizeSfx(tiro);
                }
            }
        }
    }

    public void Tiro()
    {
        Vector3 shootDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - player.transform.position;
        GameObject b = (GameObject)Instantiate(bulletPrefab, Vector3.zero, Quaternion.identity);
        b.transform.SetParent(pai.transform);
        b.transform.position = player.transform.position;
        b.transform.localScale = Vector3.one;
        b.GetComponent<Rigidbody2D>().velocity = new Vector2(shootDirection.x, shootDirection.y).normalized * bulletSpeed;
    }
}
