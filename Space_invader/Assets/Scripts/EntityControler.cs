using UnityEngine;

public class EntityControler : MonoBehaviour
{
    //Movement variables
    private float moveSpeed = 1f;
    private Rigidbody2D entityRb;
    private float moveDistance = 1f;
    private Vector3 prevPos;
    //projectile variables
    public ProjectileControler projectilePrefab;
    private float shootingTimer;
    //other variables
    private int entityColumn;


    public enum EntityTypes
    {
        None,
        EnemyBase,
        EnemyShooter
    }

    public EntityTypes entityType=EntityTypes.None;

    // Start is called before the first frame update
    void Start()
    {
        entityRb = GetComponent<Rigidbody2D>();
        shootingTimer = UnityEngine.Random.Range(4f, 7f);
        entityColumn = (int)System.Math.Round(3.5f-transform.position.x);
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        if (entityType==EntityTypes.EnemyShooter)
        {
            if (shootingTimer > 0f)
            {
                shootingTimer -= Time.deltaTime;
            }
            else
            {
                FireLaser();
                shootingTimer = UnityEngine.Random.Range((4f-GameManagerScript.instance.difficultyModifier), (7f- GameManagerScript.instance.difficultyModifier));
            }
        }
        
    }

    private void FireLaser()
    {
        ProjectileControler projectileInstance;
        projectileInstance = Instantiate(projectilePrefab, transform.position, transform.rotation) as ProjectileControler;
        projectileInstance.SetParentColumn(entityColumn);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if hit end of map destroy projectile
        if (collision.gameObject.layer == 10)
        {
            if (collision.gameObject.name == "LeftBorder")
            {
                 GameManagerScript.instance.entityDirection = 2;
            }
            else if (collision.gameObject.name == "RightBorder")
            {
                 GameManagerScript.instance.entityDirection = 0;
            }
        }
        

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "BottomBorder")
        {
            GameManagerScript.instance.MoveToScore();
        }
        if (collision.gameObject.layer == 11)
        {
            GameManagerScript.instance.score -= 2 * GameManagerScript.instance.collumnCount[entityColumn];
            GameManagerScript.instance.entityDirection = 0;
        }
    }

    //Movement function 0 - stop, 1 - left, 2 - right
    private void HandleMovement()
    {
        if (GameManagerScript.instance.entityDirection > 0)
        {
            if (GameManagerScript.instance.entityDirection == 1)
            {
                entityRb.velocity = Vector2.left * moveSpeed * (1+GameManagerScript.instance.difficultyModifier);
            }
            else
            {
                entityRb.velocity = Vector2.right * moveSpeed * (1 + GameManagerScript.instance.difficultyModifier);
            }
            //ensure that movedistance is reset on all entities
            moveDistance = 1f;
        }
        else
        {
            entityRb.velocity = new Vector2(0f, 0f);
            if (moveDistance>0f)
            {
                prevPos = transform.position;
                transform.Translate((transform.up * -1) * moveSpeed * (1 + GameManagerScript.instance.difficultyModifier) * Time.deltaTime, Space.World);
                moveDistance -= Mathf.Abs(Vector3.Distance(prevPos, transform.position));
            }
            else
            {
                GameManagerScript.instance.entityDirection = 1;
            }
            
        }

    }

    void OnDestroy()
    {
        GameManagerScript.instance.collumnCount[entityColumn] -= 1;
        GameManagerScript.instance.enemyCount -= 1;
    }
}
