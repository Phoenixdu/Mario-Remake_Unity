using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class PlayerMovement : MonoBehaviour {

    //Movement
    public Rigidbody2D rb;
    public int playerSpeed = 2;
    public bool facingRight = false;
    public int playerJumpPower = 200;
    public float moveX;
    
    //Grounded?
    public Transform groundCheckPoint;
    public float groundCheckRadius;
    public bool isGrounded;
    public LayerMask groundLayer;

    //Projectile
    public Transform projectileSpawnPoint;
    public Projectile projectilePrefab;
    public float projectileForce;
    
    //Animation
   public Animator anim;

    //Audio
    public AudioSource jumpAudio;
    public AudioSource fireballAudio;
    public AudioSource pickupAudio;
    

    private void Start()
    {
       anim = GetComponent<Animator>();
    }
   
    // Update is called once per frame
    void Update () {

        //Grounded?
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, groundLayer);

        //Projectile
        if (!Scene_Manager.gameIsPaused)
        {
          if (Input.GetButtonDown("Fire1"))
            {
                Debug.Log("Pew pew");
                fire();
            }

            PlayerMove();
        }
        //Animation
        anim.SetFloat("speed", Mathf.Abs(moveX));
        anim.SetFloat("verticalSpeed", Mathf.Abs(gameObject.GetComponent<Rigidbody2D>().velocity.y));
        

    }

    //Movement
    void PlayerMove()
    {
        //Controls
        moveX = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
            
        }

        //Direction
        if (moveX < 0.0f && facingRight == false)
        {
            FlipPlayer ();
        }
        else if (moveX > 0.0f && facingRight == true)
        {
            FlipPlayer ();
        }

        //Physics
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveX * playerSpeed, gameObject.GetComponent<Rigidbody2D>().velocity.y);
    }

    //Collectible
    void OnTriggerEnter2D(Collider2D c)
    {
        
        if (c.gameObject.tag == "Collectible")
        {
            
            Destroy(c.gameObject);
            pickupAudio.Play();
        }

    }



    //Jump
    void Jump()
    {
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * playerJumpPower);
        jumpAudio.Play();

    }

 
    //Flip
    void FlipPlayer()
    {
        facingRight = !facingRight;
        Vector2 localScale = gameObject.transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
        projectileForce *= -1;
    }

    //Projectile
    void fire()
    {
        Projectile temp = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
        temp.speed = projectileForce;
        anim.SetTrigger("isShooting");
        fireballAudio.Play();
    }

   
}

