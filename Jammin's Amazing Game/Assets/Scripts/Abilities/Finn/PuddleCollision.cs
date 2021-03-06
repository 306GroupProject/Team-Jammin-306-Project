﻿using UnityEngine;
using UnityEngine.Networking;

/*
 * Handles Puddle Collision
 */
public class PuddleCollision : NetworkBehaviour
{

    [SerializeField, SyncVar]
    public float slowRate = 50.0f;
    PlayerManager Script;
    bool notelectric = true;

    public int lifeTime;

    public GameObject steam;
    public GameObject electricPuddle;
    public GameObject tar;

    /*
     * Slows a player when they enter the puddle, and electrifies the puddle if hit by a bolt
     */
    void Start()
    {
        InvokeRepeating("restoreSpeed", 0.0f, 1.0f);
    }
    
    public void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player1" || collision.gameObject.tag == "Player2" || collision.gameObject.tag == "Player3" || collision.gameObject.tag == "Player4")
        {
            
            Script = collision.gameObject.GetComponent<PlayerManager>();
            Script.changeSpeed(slowRate);
        }
        if (collision.gameObject.tag == "Bolt" && notelectric)
        {
            Destroy(collision.gameObject);
            notelectric = false;


			// we are just creating the ePuddle, does not mean we need to use the variable. 
            Instantiate(electricPuddle, transform.position, Quaternion.identity);
		

            Script.changeSpeed(100.0f);
            Destroy(this.gameObject);          
        }

		
		if (collision.gameObject.tag.Equals ("bossL")) {
			GameObject.FindGameObjectWithTag("bossL").GetComponent<bossAi>().castAttackSpeed = true; 
		}

        if (collision.gameObject.tag == "Boulder") {
            Instantiate(tar, transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }

    /*
    * Restores the players original speed when they leave the puddle
    */
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player1" || collision.gameObject.tag == "Player2" || collision.gameObject.tag == "Player3" || collision.gameObject.tag == "Player4") {
            Script = collision.gameObject.GetComponent<PlayerManager>();
            Script.changeSpeed(100.0f);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Fireball")
        { 
            Destroy(collision.gameObject);
            GameObject steamer = Instantiate(steam, this.transform.position, Quaternion.identity);
            Destroy(steamer.gameObject, steamer.GetComponent<ParticleSystem>().main.duration / 2.0f);
            if (Script)
            {
                Script.changeSpeed(100.0f);
            }
            
            Destroy(this.gameObject);
        }
    }

    private void restoreSpeed()
    {
        lifeTime = lifeTime - 1;
        if (lifeTime < 0)
        {
            Script.changeSpeed(100.0f);
            Destroy(this.gameObject);
        }
    }
}
