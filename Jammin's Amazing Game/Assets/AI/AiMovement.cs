using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking; 
using UnityEngine;

public class AiMovement : NetworkBehaviour {

    private Animator anim;
    private Rigidbody2D rb; // used to find the velocity of the enemy so that it can be animated

	private GameObject playerPosition;	// this will eventually need to be changed to a array to find all the players. Yet to test with multiplayer. 
	public float speed; 				// change this if you want AI move faster.
	public int aiDmg = 4; 

	public void OnCollisionEnter2D(Collision2D collision){


		if (collision.gameObject.tag == "Player") {


			playerPosition.GetComponent<playerHealth> ().Damage (aiDmg);

		}

	}

	public void OnTriggerEnter2D(Collider2D coll){

		 if (coll.gameObject.tag == "basicAttack") {
			// we can change were damage goes later. I just put it in there for now.
			this.GetComponent<aiHealth>().Damage(playerPosition.GetComponent<CharacterMovement>().plyerDmg);
            anim.SetTrigger("Hurt"); // play the hurt animation
			
		}

	}


	// Use this for initialization
	void Start () {

        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {

        if (playerPosition == null)
        {

            // we will have to find a better way to fix this. For now the AI can find the position of the player. 
            // The problem is that no player exists at Start, so we cant initialize the player position until the game 
            // is running. 
            playerPosition = GameObject.FindWithTag("Player");

        }

        // if the Vector2 position is less then 7f distance, we will apply force to move towards Player. 
        if (Vector2.Distance(this.transform.position, playerPosition.transform.position) < 7f)
        {

            this.transform.position = Vector2.MoveTowards(this.transform.position, playerPosition.transform.position, speed * Time.deltaTime);


        }

        if (Mathf.Abs(rb.velocity.x) > 0.1 || Mathf.Abs(rb.velocity.y) > 0.1) // if the enemy is moving, animate it walking
        {
            anim.SetBool("IsMoving", true);
        }
        else
        {
            anim.SetBool("IsMoving", false);
        }

    }

}
