using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D body;
    private float horizontal, vertical, moveLimiter = 0.7f;
    public float runSpeedOriginal, runSpeed = 1;

    private Vector2 movement;

    private MissionManager mManager;
    private UpgradeManager uManager;
    private GameManager gManager;

    protected bool facingRight;

    public GameObject fogPrefab, spriteMask, spaceText;

    private int layerMask;

    public bool pertoIlha = false;

    void Start()
    {
        layerMask = 1 << 8;
        facingRight = true;
        uManager = GameObject.FindGameObjectWithTag("UpgradeManager").GetComponent<UpgradeManager>();
        mManager = GameObject.FindGameObjectWithTag("MissionManager").GetComponent<MissionManager>();
        gManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        body = GetComponent<Rigidbody2D>();
        runSpeedOriginal = runSpeed;
    }

    void Update()
    {
        if(gManager.currentState == GameManager.GameState.Gameplay && pertoIlha)
        {
            if (GetComponent<Rigidbody2D>().velocity.magnitude > 0.1f)
            {
                spaceText.GetComponent<TextMeshProUGUI>().SetText("Slowdown");
                spaceText.SetActive(true);
            }
            else if((GetComponent<Rigidbody2D>().velocity.magnitude <= 0.1f))
            {
                spaceText.GetComponent<TextMeshProUGUI>().SetText("Space to dock");
                spaceText.SetActive(true);
            }
        }
        else
        {
            spaceText.SetActive(false);
        }
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.zero, Mathf.Infinity, layerMask);
        if (hit.collider != null)
        {
        }
        else
        {
            Instantiate(fogPrefab, transform.position, Quaternion.identity);
        }

        if (uManager.nivelBarco == 1)
        {
            runSpeed = 1f;
            spriteMask.transform.localScale = new Vector3(100, 100, 0);
            fogPrefab.transform.localScale = new Vector3(25, 25, 0);
        }
        if (uManager.nivelBarco == 2)
        {
            runSpeed = 1.5f;
            spriteMask.transform.localScale = new Vector3(160, 160, 0);
            fogPrefab.transform.localScale = new Vector3(40, 40, 0);
        }
        if (uManager.nivelBarco == 3)
        {
            runSpeed = 2f;
            spriteMask.transform.localScale = new Vector3(220, 220, 0);
            fogPrefab.transform.localScale = new Vector3(55, 55, 0);
        }
    }

    public void ChangeDirection()
    {
        facingRight = !facingRight;
        this.GetComponent<SpriteRenderer>().flipX = !facingRight;
    }
    private void Flip(float horizontal)
    {
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            ChangeDirection();
        }
    }
    void FixedUpdate()
    {
        if (gManager.playerPodeAndar)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            if (horizontal != 0 && vertical != 0)
            {
                horizontal *= moveLimiter;
                vertical *= moveLimiter;
            }
            movement = new Vector2(horizontal, vertical);
            body.AddForce(movement * runSpeed);
            Flip(horizontal);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ilha")
        {
            mManager.ilhaAtiva = collision.gameObject;
            runSpeedOriginal = runSpeed;
            runSpeed = runSpeed / 2;
        }
        if (collision.gameObject.tag == "Porto")
        {
            runSpeedOriginal = runSpeed;
            runSpeed = runSpeed / 2;
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ilha" || collision.gameObject.tag == "Porto")
        {
            pertoIlha = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ilha" || collision.gameObject.tag == "Porto")
        {
            runSpeed = runSpeedOriginal;
            pertoIlha = false;
        }
    }
}
