using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public float speed;
    public float lifeTime;

    // Use this for initialization
    void Start() {

        if (speed == 0)
        {
            speed = 7.0f;
        }

        if (lifeTime <= 0)
        {
            lifeTime = 1.0f;
        }

        GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
        Destroy(gameObject, lifeTime);
    }

 
	
	// Update is called once per frame
	void Update () {

		
	}

    void OnCollisionEnter(Collision c)
    {
        Destroy(gameObject);
    }
}
