using System;
using UnityEngine;

public class ProjectileControler : MonoBehaviour
{
    [SerializeField]
    private bool isPlayerProjectile = false;
    private Rigidbody2D projeticleRb;
    private float projectileSpeed = 200f;
    private int parentColumn=0;

    public int GetParentColumn()
    {
        return parentColumn;
    }

    public void SetParentColumn(int value)
    {
        parentColumn = value;
    }

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.Play("LaserSFX");
        projeticleRb = GetComponent<Rigidbody2D>();
        if (isPlayerProjectile)
        {
            projeticleRb.AddForce(transform.up * projectileSpeed);
        }
        else
        {
            projeticleRb.AddForce(transform.up * projectileSpeed * -1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        // if hit end of map destroy projectile
        if (collision.gameObject.layer == 10)
        {
            Destroy(gameObject);
        }
        // if player hit enemy destroy it and projectile
        if (collision.gameObject.layer == 9 && isPlayerProjectile)
        {
            AudioManager.instance.Play("ExplosionSFX");
            GameManagerScript.instance.difficultyModifier += 0.05f;
            Destroy(collision.gameObject);
            GameManagerScript.instance.score += 1;
            GameManagerScript.instance.UiUpdateEvent(this, EventArgs.Empty);
            Destroy(gameObject);
        }
        // if hit player destroy projectile and remove score equal to 2*column from hit originated
        if (collision.gameObject.layer == 11 && !isPlayerProjectile)
        {
            GameManagerScript.instance.DeathEvent(this, EventArgs.Empty);
            Destroy(gameObject);
        }
    }
}
