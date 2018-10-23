using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking; 
using UnityEngine;

public class ai : NetworkBehaviour {

	private GameObject playerPosition; 
	//private Rigidbody2D rb; // used to find the velocity of the enemy so that it can be animated
	private Animator anim;

	public int aiMovementSpeed = 0;  // aiMovement will need to be set in unity. 
	public int aiDmg = 0; 		// aiDmg, will need to be set in unity
	public decisionTree rootOfTree;  // reference to Decision Tree, which points at root.




	public void OnCollisionEnter2D(Collision2D collision){
		
		if (collision.gameObject.tag == "Player") {
			playerPosition.GetComponent<playerHealth> ().Damage (aiDmg);
		}
		
	}
	
	public void OnTriggerEnter2D(Collider2D coll){
		
		if (coll.gameObject.tag == "basicAttack") {
            // we can change were damage goes later. I just put it in there for now.
            Destroy(coll.gameObject);
			this.GetComponent<aiHealth>().Damage(playerPosition.GetComponent<PlayerManager>().plyerDmg); 
			//anim.SetTrigger("Hurt"); // play the hurt animation

		}
		
	}


//------------[[Decisions: Boolean]]------------------//
	public bool enemySpotted(){
		playerPosition = GameObject.FindWithTag("Player2");
		// is enemySpotted this is our first decision!
		if (Vector2.Distance (this.transform.position, playerPosition.transform.position) < 10f) {
            anim.SetBool("IsMoving", true);
			return true; 

		
		} else {
            anim.SetBool("IsMoving", false);
            return false; 

		}




	}

//-----------[[Actions: Void ]]-----------//
	public void Movement(){
		//print ("Player is spotted!"); 
		this.aiMovementSpeed = 4; 

		// if the Vector2 position is less then 7f distance, we will apply force to move towards Player. 

		this.transform.position = Vector2.MoveTowards(this.transform.position, playerPosition.transform.position, aiMovementSpeed * Time.deltaTime); 
			
		//if (Mathf.Abs(rb.velocity.x) > 0.1 || Mathf.Abs(rb.velocity.y) > 0.1) // if the enemy is moving, animate it walking
		//{
		//	anim.SetBool("IsMoving", true);
		//}
		//else
		//{
		//	anim.SetBool("IsMoving", false);
		//}

			
	}

	public void idle(){
		this.aiMovementSpeed = 0; 
		//print ("I am idle now!");

	}


	// build our tree from the ground up. 
	public void buildDecisionTree(){

		decisionTree inRangeNode = new decisionTree (); 
		inRangeNode.buildDecision(enemySpotted); 

		decisionTree MoveTowardsEnemy = new decisionTree ();
		MoveTowardsEnemy.buildAction(Movement);

		decisionTree idle = new decisionTree (); 
		idle.buildAction(this.idle); 



		inRangeNode.Left (idle); 
		inRangeNode.Right (MoveTowardsEnemy); 

		rootOfTree = inRangeNode; 
	}




	// Use this for initialization
	void Start () {

		buildDecisionTree ();
		//rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();

	}
	
	// Update is called once per frame
	void Update () {

		rootOfTree.search (); 
		
	}


}
