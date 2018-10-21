using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking; 
using UnityEngine;

public class ai : NetworkBehaviour {
	[SerializeField]
	private GameObject [] playerPosition; 
	private bool builtPlayerPos = false; 
	private int playerSpotted = -1; 

//	private Rigidbody2D rb; // used to find the velocity of the enemy so that it can be animated
	private Animator anim;
	private GameObject networkManager; 
	public int aiMovementSpeed = 0;  // aiMovement will need to be set in unity. 
	public int aiDmg = 0; 		// aiDmg, will need to be set in unity
	public decisionTree rootOfTree;  // reference to Decision Tree, which points at root.



	/**
	 *  void OnCollisionEnter2D():
	 * When the player collides with the AI, the ai has attacked causing damage to the player.
	 * 
	 * param: collision2d collsion: the object that the AI is colliding with. 
	 * 
	 * return: Nothing
	 */
	public void OnCollisionEnter2D(Collision2D collision){
		print ("dmg: "+aiDmg);
		
		if (collision.gameObject.tag == "Player1") {

			playerPosition [0].GetComponent<playerHealth> ().Damage (aiDmg);
			
		} 
		
		if (collision.gameObject.tag == "Player2") {

			playerPosition [1].GetComponent<playerHealth> ().Damage (aiDmg);

		} 

		if (collision.gameObject.tag == "Player3") {

			playerPosition [2].GetComponent<playerHealth> ().Damage (aiDmg);
		}

		if (collision.gameObject.tag == "Player4") {

			playerPosition [3].GetComponent<playerHealth> ().Damage (aiDmg);

		}
		
	}

	public void initPlayerPos(){
		this.playerPosition = networkManager.GetComponent<CustomNetwork> ().returnPlayers ();  
	}

	/**
	 *  void OnTriggerEnter2D():
	 * When the player shoots a attack at the AI, then it will trigger damage.
	 * on the AI unit. 
	 * 
	 * param: collision2d coll: the object that is colliding into the Ai.
	 * 
	 * return: Nothing
	 */

	public void OnTriggerEnter2D(Collider2D coll){
		
		if (coll.gameObject.tag == "basicAttack") {
			// we can change were damage goes later. I just put it in there for now.

			this.GetComponent<aiHealth>().Damage(playerPosition[0].GetComponent<CharacterController>().plyerDmg); 
			anim.SetTrigger("Hurt"); // play the hurt animation

		}
		
	}


//------------[[Decisions: Boolean]]------------------//

// If you are adding either a decision or action to the AI, keep in mind that the data type of the function matters.
// all decisions tell the tree what to do, so that means all decisions needs to be booleans and return a true or false.
// all actions are void, meaning they returning nothing but instead change the state of the AI in some way.

	/**
	 * bool enemySpotted():
	 * when the enemy is within distance of the AI it this function will return either True or False.
	 * 
	 * return: True if enemy is spotted or false if enemy is not spotted. 
	 */ 
	public bool enemySpotted(){

		// is enemySpotted this is our first decision!

		if (playerPosition [0] != null) {

			if (Vector2.Distance (this.transform.position, playerPosition [0].transform.position) < 10f) {

				anim.SetBool ("IsMoving", true);
				playerSpotted = 0; 

				return true;

			} else {

				anim.SetBool ("IsMoving", false);
				return false;

			}
		
		} else if (playerPosition [1] != null) {

			if (Vector2.Distance (this.transform.position, playerPosition [1].transform.position) < 10f) {
		
				anim.SetBool ("IsMoving", true);

				playerSpotted = 1; 

				return true; 

			} else {

				anim.SetBool ("IsMoving", false);
				return false;
			}

		} else if (playerPosition [2] != null) {
				
			if (Vector2.Distance (this.transform.position, playerPosition [2].transform.position) < 10f) {
					
				anim.SetBool ("IsMoving", true);
				playerSpotted = 2; 
				return true; 
					
			} else {
					
				anim.SetBool ("IsMoving", false);
				return false;
			}

		} else if (playerPosition [3] != null) {
			
			if (Vector2.Distance (this.transform.position, playerPosition [3].transform.position) < 10f) {
				
				anim.SetBool ("IsMoving", true);
				playerSpotted = 3; 
				return true; 
				
			} else {
				
				anim.SetBool ("IsMoving", false);
				return false;
			}

		} else {


			return false; 
		}

		
		

}

//-----------[[Actions: Void ]]-----------//

	/**
	 * void Movement(): 
	 * move towards the player to attack!
	 * 
	 * returns: Nothing
	 */ 
	public void Movement(){

		// if the Vector2 position is less then 7f distance, we will apply force to move towards Player. 
		this.aiMovementSpeed = 4; 
		this.transform.position = Vector2.MoveTowards(this.transform.position, playerPosition[playerSpotted].transform.position, aiMovementSpeed * Time.deltaTime); 
			
		//if (Mathf.Abs(rb.velocity.x) > 0.1 || Mathf.Abs(rb.velocity.y) > 0.1) // if the enemy is moving, animate it walking
		//{
		//	anim.SetBool("IsMoving", true);
		//}
		//else
		//{
		//	anim.SetBool("IsMoving", false);
		//}

			
	}

	
	/**
	 * void idle(): 
	 * If player is not within line of sight, then stand still FOR NOW :(
	 * 
	 * returns: Nothing
	 */
	public void idle(){
		this.aiMovementSpeed = 0; 


	}





	/**
	 * void buildDecisionTree(): 
	 * 
	 * This functions builds the entire AIS decision tree, what ever we want the ai to do we must make a node for it.
	 * Each action should have a decision leading behind it. The node of the decision tree has something called a 
	 * delegate within it. So if the node you are building has a decision assign that decision function to the deleagate.
	 * 
	 * return: Nothing
	 * 
	 */ 
	public void buildDecisionTree(){

		decisionTree inRangeNode = new decisionTree ();  // create the root of our tree.
		inRangeNode.buildDecision(enemySpotted);  		// the root of this tree is a Decision it is if the enemy is spotted or not.

		decisionTree MoveTowardsEnemy = new decisionTree (); //the second node is a action.
		MoveTowardsEnemy.buildAction(Movement);				// assign the node to a action within the Tree.

		decisionTree idle = new decisionTree (); 		// the third node is a Action.
		idle.buildAction(this.idle); 					// assign the node to a action.



		inRangeNode.Left (idle); 					// the root is now in need of its children. Assign the false choice to idle.
		inRangeNode.Right (MoveTowardsEnemy); 		// true choice is to move towards the enemy.

		rootOfTree = inRangeNode; 					// set the root.
	}





	// Use this for initialization
	void Start () {
		this.aiMovementSpeed = 4; 
		buildDecisionTree ();
	//	rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		networkManager = GameObject.FindGameObjectWithTag("networkManager"); 

	}
	
	// Update is called once per frame
	void Update () {

		if (networkManager.GetComponent<CustomNetwork>().players.Length != 0 && builtPlayerPos == false) {

			this.initPlayerPos(); 
			
			
		}

		rootOfTree.search ();  // search for our choice to do. ie update

	
		
	}


}
