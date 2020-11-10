using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    //Movement variables
    private float moveSpeed = 3f;
    private Rigidbody2D playerRb;
    private int playerDirection = 0;
    private float shootingTimer = 2.0f;
    private float powerUpTimer = 5f;
    //other variables
    public Rigidbody2D projectilePrefab;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
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
}
