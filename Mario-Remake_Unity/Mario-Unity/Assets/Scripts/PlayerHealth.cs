using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour {

    public int health;
    

    //Audio
    public AudioSource gameoverAudio;
    public AudioSource playerHit;

	// Use this for initialization
	void Start () {
        if (health == 0)
        {
            health = 3;
        }		
	}

    //Handles falling off the map
    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag == "Death_Zone")
        {
            StartCoroutine("Die");
        }
    }
   
    //Enemy
    void OnCollisionEnter2D(Collision2D c)
    {

        //Collision with enemy or projectile
        if (c.gameObject.tag == "Enemy" || c.gameObject.tag == "Enemy_Projectile")
        {
            health--;
            Debug.Log("Health is " + health);

            StartCoroutine("PlayerHit");
         
            if (health <= 0)
            {
                StartCoroutine("Die");
                
            }        
        }
    }

    IEnumerator Die()
    {
        GameObject.Find("LevelMusic").GetComponent<AudioSource>().Stop();
        gameoverAudio.Play();
        print("SHOULD PLAY AUDIO");
        yield return new WaitForSecondsRealtime(4);
        SceneManager.LoadScene("Start");
        
    }

    IEnumerator PlayerHit()
    {
        Time.timeScale = 0f;
        GameObject.Find("LevelMusic").GetComponent<AudioSource>().Stop();
        playerHit.Play();
        yield return new WaitForSecondsRealtime(3);
        Time.timeScale = 1f;
        SceneManager.LoadScene("Mario");
    }
}
