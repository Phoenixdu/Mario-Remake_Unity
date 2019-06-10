using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Enemy_Turtle : MonoBehaviour {

    //health
    public int health;
    

    //Handles movemenet
    public Rigidbody2D rb;
    public bool facingRight;
    public float speed;
    public float distance;

    //Handles Audio
    public AudioSource enemyDeath;


     void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (speed <= 0)
        {
            
            speed = 1.0f;

            // Prints a message to Console (Shortcut: Control+Shift+C)
            Debug.LogWarning("Default speed to " + speed);
        }
    }

    void Update()
    {

        if (!facingRight)
            // Move Enemy Left
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        else
            // Move Enemy Right
            rb.velocity = new Vector2(speed, rb.velocity.y);
    }

    void OnCollisionEnter2D(Collision2D c)
    {
        
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
                enemyDeath.Play();

            }
        }

        if(c.gameObject.tag != "Ground")
        {
            flip();
        }
    }

    void flip()
    {
        facingRight = !facingRight;

        Vector2 scaleFactor = transform.localScale;

        //Flip scale
        scaleFactor.x *= -1;

        //update scale to flipped scale
        transform.localScale = scaleFactor;
    }
}
