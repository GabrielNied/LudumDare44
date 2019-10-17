using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject pai, enemyPrefab;
    public float cooldown = 1f;
    public int quantidadeEnemy, dificuldade;
    public Sprite enemySprite;

    public GameManager gManager;

    void Start()
    {
        gManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        if (gManager.currentState == GameManager.GameState.Battle)
        {
            if (cooldown < 1f)
            {
                cooldown += Time.deltaTime * dificuldade /1.5f;
            }
            else if(cooldown >= 1f && quantidadeEnemy > 0)
            {
                Spawn();
                cooldown = Random.Range(-2f, 0);                
            }
        }
    }

    public void Spawn()
    {
        GameObject enemy = (GameObject)Instantiate(enemyPrefab, Vector3.zero, Quaternion.identity);
        enemy.transform.SetParent(pai.transform);
        enemy.transform.localPosition = new Vector3 (Random.Range(-500, 500), 450, 0);
        enemy.transform.localScale = new Vector3 (3,3,0);
        enemy.GetComponent<Image>().sprite = enemySprite;
        enemy.GetComponent<Enemy>().velocidade = dificuldade / 1.5f;
        quantidadeEnemy -= 1;
    }
}
