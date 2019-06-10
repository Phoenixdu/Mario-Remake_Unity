using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Enemy_ranged : MonoBehaviour {

    // Handles projectile creation
    public Transform projectileSpawnPoint;
    public enemy_projectile projectilePrefab;
    public float projectileForce;

    // Handles projectile fire 
    public float projectileFireRate;
    float timeSinceLastFire = 0.0f;

    //Handles shooting AI
    public bool shootLeft;
    public GameObject target = null;

    //Handles flipping
    public bool facingRight = false;

    // Handles Enemy health
    public int health;

    //Handles audio
    public AudioSource bowserDeath;

    // Use this for initialization
    void Start()
    {
        //Projectile force
        if (projectileForce == 0)
        {
        
            projectileForce = 3.0f;

        }

        //Projectilefire rate
        if (projectileFireRate == 0)
        {
            projectileFireRate = 5.0f;
        }

       //Enemy health
        if (health == 0)
        {
           
            health = 2;
        }

        if (!target)
            target = GameObject.FindWithTag("Player");
        shootDirectionCheck();

    }




    //Create and spawn projectile
    void fire()
    {

        shootDirectionCheck();

        if (shootLeft)
        {
            enemy_projectile temp = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
            temp.speed = -projectileForce;
        }

        else
        {
            enemy_projectile temp = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
            temp.speed = projectileForce;
        }
        
    }

    //Flipping 
    void flip()
    {
        facingRight = !facingRight;

        Vector2 scaleFactor = transform.localScale;

        //Flip scale
        scaleFactor.x *= -1;

        transform.localScale = scaleFactor;

    }

    //Shooting direction
    void shootDirectionCheck()
    {
        // Is target on Left or Right side?
        if (target.transform.position.x < transform.position.x)
        {
            shootLeft = true; // Shoot left

            if (facingRight)
            {
                flip();
            }
            
        }
            
        else
        {
            shootLeft = false; // Shoot right

            if (!facingRight)
            {
                flip();
            }

        }

    }
 
    //Shooting time
    void OnTriggerStay2D(Collider2D c)
    {
        if (c.gameObject.tag == "Player")
        {
            if (Time.time > timeSinceLastFire + projectileFireRate)
            {
                fire();
                timeSinceLastFire = Time.time;
            }
        }

        if (c.gameObject.tag == "Projectile")
        {
            // Destroy Projectile that collided
            Destroy(c.gameObject);

            health -= 1; // Remove 1 health on Collision
            
            // Check if health is ZERO
            if (health <= 0)
            {
                // Destroy GameObject Script is attached to
                // when health is ZERO (Enemy)
                Destroy(gameObject);
                bowserDeath.Play();
            }
        }
    }
}
