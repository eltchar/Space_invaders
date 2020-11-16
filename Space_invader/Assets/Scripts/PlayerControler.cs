using System;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    //Movement variables
    private float moveSpeed = 3f;
    private Rigidbody2D playerRb;
    private int playerDirection = 0;
    //shooting variables
    private float shootingTimer = 2.0f;
    private float powerUpTimer = 5f;
    //other variables
    public Rigidbody2D projectilePrefab;
    private Vector3 startingPos;
    private float deathTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        startingPos = transform.position;
        GameManagerScript.instance.OnDeathEvent += PlayerDeath;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        if (shootingTimer > 0f)
        {
            shootingTimer -= Time.deltaTime;
        }
        else
        {
            FireLaser();
        }
        if (powerUpTimer > 0f && GameManagerScript.instance.powerUpEnabled)
        {
            powerUpTimer -= Time.deltaTime;
        }
        else if(GameManagerScript.instance.powerUpEnabled)
        {
            GameManagerScript.instance.powerUpEnabled = false;
            powerUpTimer = 5f;
        }
        if (deathTimer > 0f)
        {
            deathTimer -= Time.deltaTime;
        }


    }
    public void SetDirection(int direction)
    {
        playerDirection = direction;
    }

    private void FireLaser()
    {
        Rigidbody2D projectileInstance;
        Vector3 shipFront = new Vector3(transform.position.x, transform.position.y+0.5f, transform.position.z);
        projectileInstance = Instantiate(projectilePrefab, shipFront, transform.rotation) as Rigidbody2D;
        if (GameManagerScript.instance.powerUpEnabled)
        {
            shootingTimer = 1.0f;
        }
        else
        {
            shootingTimer = 2.0f;
        }
    }

    //Movement function 0 - stop, 1 - left, 2 - right
    private void HandleMovement()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.position.x > Screen.width / 2)
            {
                playerDirection = 2;
            }
            else
            {
                playerDirection = 1;
            }
        }
        else
        {
            playerDirection = 0;
        }
        //if direction is non 0
        if (playerDirection > 0)
        {
            if (playerDirection == 1)
            {
                playerRb.velocity = Vector2.left * moveSpeed;
            }
            else
            {
                playerRb.velocity = Vector2.right * moveSpeed;
            }
        }
        //if nothing is pressed set velocity to 0
        else
        {
            playerRb.velocity = Vector2.zero;
        }
    }

    private void PlayerDeath(object sender, EventArgs e)
    {
        if (deathTimer<=0)
        {
            if (sender.GetType().ToString() == "ProjectileControler")
            {
                ProjectileControler projectile = sender as ProjectileControler;
                GameManagerScript.instance.score -= 2 * GameManagerScript.instance.collumnCount[projectile.GetParentColumn()];
                AudioManager.instance.Play("ShieldHitSFX");
                playerRb.velocity = new Vector2(0f, 0f);
                transform.position = startingPos;
                deathTimer = 2f;
                GameManagerScript.instance.liveCount -= 1;
                GameManagerScript.instance.UiUpdateEvent(this, EventArgs.Empty);
            }
        }
    }

    private void OnDestroy()
    {
        GameManagerScript.instance.OnDeathEvent -= PlayerDeath;
    }
}
