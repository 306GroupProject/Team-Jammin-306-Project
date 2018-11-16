using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meleeAi : ai {

	public float spottingDistance;

	/**
	 *  void OnCollisionEnter2D():
	 * When the player collides with the AI, the ai has attacked causing damage to the player.
	 * 
	 * param: collision2d collsion: the object that the AI is colliding with. 
	 * 
	 * return: Nothing
	 */
	public void OnCollisionEnter2D(Collision2D collision){
		
		
		
		if (collision.gameObject.tag == "Player1") { // check to see which player collided with the melee AI.
			
			int counter = 0; 
			
			while(counter < plyController.GetComponent<netWorkAssitant>().playerManager.Count){ // if it is player one, then lets count through our sync list to find their gameobject.
				
				if(plyController.GetComponent<netWorkAssitant>().playerManager[counter].ply.gameObject.tag == "Player1"){
					
					plyController.GetComponent<netWorkAssitant>().playerManager[counter].ply.gameObject.GetComponent<playerHealth> ().Damage (aiDmg);
				}
				
				counter ++; 
				
			}
			
		} 
		
		if (collision.gameObject.tag == "Player2") {
			
			int counter = 0; 
			
			while(counter < plyController.GetComponent<netWorkAssitant>().playerManager.Count){
				
				if(plyController.GetComponent<netWorkAssitant>().playerManager[counter].ply.gameObject.tag == "Player2"){
					
					plyController.GetComponent<netWorkAssitant>().playerManager[counter].ply.gameObject.GetComponent<playerHealth> ().Damage (aiDmg);
				}
				
				counter ++; 
				
			}
			
			
		} 
		
		if (collision.gameObject.tag == "Player3") {
			
			int counter = 0; 
			
			while(counter < plyController.GetComponent<netWorkAssitant>().playerManager.Count){
				
				if(plyController.GetComponent<netWorkAssitant>().playerManager[counter].ply.gameObject.tag == "Player3"){
					
					plyController.GetComponent<netWorkAssitant>().playerManager[counter].ply.gameObject.GetComponent<playerHealth> ().Damage (aiDmg);
				}
				
				counter ++; 
				
			}
			
		}
		
		if (collision.gameObject.tag == "Player4") {
			
			
			int counter = 0; 
			
			while(counter < plyController.GetComponent<netWorkAssitant>().playerManager.Count){
				
				if(plyController.GetComponent<netWorkAssitant>().playerManager[counter].ply.gameObject.tag == "Player4"){
					
					plyController.GetComponent<netWorkAssitant>().playerManager[counter].ply.gameObject.GetComponent<playerHealth> ().Damage (aiDmg);
				}
				
				counter ++; 
				
			}
			
		}
		
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
		
		if (coll.gameObject.tag == "basicAttack" || coll.gameObject.layer == 13) {
			Destroy(coll.gameObject);
			// we can change were damage goes later. I just put it in there for now.
			this.GetComponent<aiHealth>().Damage(plyerDmg); 
			anim.SetTrigger("Hurt"); // play the hurt animation
			
		}
		
	}
	
	
	//------------[[Decisions: Boolean]]------------------//
	
	// If you are adding either a decision or action to the AI, keep in mind that the data type of the function matters.
	// all decisions tell the tree what to do, so that means all decisions needs to be booleans and return a true or false.
	// all actions are void, meaning they returning nothing but instead change the state of the AI in some way.
	
	/**
	 * bool enemySpotted():
	 * when the enemy is within distance of the AI this function will return either True or False.
	 * 
	 * return: True if enemy is spotted or false if enemy is not spotted. 
	 */ 
	public bool enemySpotted(){
		if (!isServer) {
			return false; 
			
		}

		// we will have multiply different cases in networking.
		// in some games there might be 1 player, in others all four will be within the game.

		// this is for single player:
		if (plyController.GetComponent<netWorkAssitant> ().playerManager.Count == 1) { // check to see if game is single player.
			GameObject ply0 = plyController.GetComponent<netWorkAssitant> ().playerManager [0].ply.gameObject; // aquire the information on playerOne.
			
			if (Vector2.Distance (this.transform.position, ply0.transform.position) < spottingDistance) { // if this player within a value of 10, then we will return true to spotting the enemy/player.
				
				this.playerSpotted = 0; 
				return true;
				
			} else {
				
				return false; // else if the player is not within spottingDistance then we return false.
			}
			
		} else if (plyController.GetComponent<netWorkAssitant> ().playerManager.Count == 2) { 	// check to see if the game has two players in the game.
			
			GameObject ply1 = plyController.GetComponent<netWorkAssitant> ().playerManager [1].ply.gameObject;
			GameObject ply0 = plyController.GetComponent<netWorkAssitant> ().playerManager [0].ply.gameObject;
			
			if (Vector2.Distance (this.transform.position, ply1.transform.position) < spottingDistance && Vector2.Distance (this.transform.position, ply0.transform.position) > spottingDistance) { // determine which player is closer to the melee AI.
				
				this.playerSpotted = 1;  // if player 2, then lets go towards that player.
				return true;
				
				
			} else if (Vector2.Distance (this.transform.position, ply0.transform.position) < spottingDistance) {
				
				this.playerSpotted = 0;  // else if it player one is closer lets head towards player One.
				return true;
				
			} else {
				
				return false; // else lets return false.
			}

			// check to see if we are now playing with three players.
			
		} else if (plyController.GetComponent<netWorkAssitant> ().playerManager.Count == 3) {
			GameObject ply0 = plyController.GetComponent<netWorkAssitant> ().playerManager [0].ply.gameObject;
			GameObject ply1 = plyController.GetComponent<netWorkAssitant> ().playerManager [1].ply.gameObject;
			GameObject ply2 = plyController.GetComponent<netWorkAssitant> ().playerManager [2].ply.gameObject;
			

			// check to see which player is closer to the AI, that being either player1,player2,orplayer3.
			if (Vector2.Distance (this.transform.position, ply1.transform.position) < spottingDistance && Vector2.Distance (this.transform.position, ply0.transform.position) > spottingDistance && Vector2.Distance (this.transform.position, ply2.transform.position) > spottingDistance) {
				
				this.playerSpotted = 1; 
				return true;
				
				
			} else if (Vector2.Distance (this.transform.position, ply0.transform.position) < spottingDistance && Vector2.Distance (this.transform.position, ply1.transform.position) > spottingDistance && Vector2.Distance (this.transform.position, ply2.transform.position) > spottingDistance) {
				
				this.playerSpotted = 0; 
				return true;
				
			} else if (Vector2.Distance (this.transform.position, ply2.transform.position) < spottingDistance && Vector2.Distance (this.transform.position, ply1.transform.position) > spottingDistance && Vector2.Distance (this.transform.position, ply0.transform.position) > spottingDistance) {
				
				this.playerSpotted = 2; 
				return true;
				
			} else {
				
				return false;
			}
			
			
			
			// playing with four players. (server only has 4 player slots).
		} else if (plyController.GetComponent<netWorkAssitant> ().playerManager.Count == 4) {
			GameObject ply0 = plyController.GetComponent<netWorkAssitant> ().playerManager [0].ply.gameObject;
			GameObject ply1 = plyController.GetComponent<netWorkAssitant> ().playerManager [1].ply.gameObject;
			GameObject ply2 = plyController.GetComponent<netWorkAssitant> ().playerManager [2].ply.gameObject;
			GameObject ply3 = plyController.GetComponent<netWorkAssitant> ().playerManager [3].ply.gameObject;
			
			
			if (Vector2.Distance (this.transform.position, ply1.transform.position) < spottingDistance && Vector2.Distance (this.transform.position, ply0.transform.position) > spottingDistance && Vector2.Distance (this.transform.position, ply2.transform.position) > spottingDistance && Vector2.Distance (this.transform.position, ply3.transform.position) > spottingDistance) {
				
				this.playerSpotted = 1; 
				return true;
				
				
			} else if (Vector2.Distance (this.transform.position, ply0.transform.position) < spottingDistance && Vector2.Distance (this.transform.position, ply1.transform.position) > spottingDistance && Vector2.Distance (this.transform.position, ply2.transform.position) > spottingDistance && Vector2.Distance (this.transform.position, ply3.transform.position) > spottingDistance) {
				
				this.playerSpotted = 0; 
				return true;
				
			} else if (Vector2.Distance (this.transform.position, ply2.transform.position) < spottingDistance && Vector2.Distance (this.transform.position, ply1.transform.position) > spottingDistance && Vector2.Distance (this.transform.position, ply0.transform.position) > spottingDistance && Vector2.Distance (this.transform.position, ply3.transform.position) > spottingDistance) {
				
				this.playerSpotted = 2; 
				return true;
				
			} else if (Vector2.Distance (this.transform.position, ply3.transform.position) < spottingDistance && Vector2.Distance (this.transform.position, ply1.transform.position) > spottingDistance && Vector2.Distance (this.transform.position, ply0.transform.position) > spottingDistance && Vector2.Distance (this.transform.position, ply2.transform.position) > spottingDistance ){ 
				
				this.playerSpotted = 3;
				return true; 
				
			} else {
				
				return false;
			}
			
			
			
			
		}
		
		
		return false; 
		
		
	}
	
	//-----------[[Actions: Void ]]-----------//
	
	/**
	 * void Movement(): 
	 * move towards the player to attack!
	 * 
	 * returns: Nothing
	 */ 
	public void Movement(){
		
		if (this.playerSpotted != -1) {
			
			aiMovementSpeed = aiSavedSpeed; 
			// if the Vector2 position is less then 7f distance, we will apply force to move towards Player. 
			this.transform.position = Vector2.MoveTowards (this.transform.position, plyController.GetComponent<netWorkAssitant> ().playerManager [this.playerSpotted].ply.gameObject.transform.position, aiMovementSpeed * Time.deltaTime);
			anim.SetBool("IsMoving", true);
		}
		
	}
	
	
	/**
	 * void idle(): 
	 * If player is not within line of sight, then stand still FOR NOW :(
	 * 
	 * returns: Nothing
	 */
	public void idle(){
		if (isServer) {
			this.aiMovementSpeed = 0f;
			anim.SetBool("IsMoving", false);
		}
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
		
		plyerDmg = 4; 
		buildDecisionTree ();
		anim = GetComponent<Animator>();
		plyController = GameObject.FindGameObjectWithTag("assistantNet");
		aiSavedSpeed = aiMovementSpeed; 
		
	}
	
	// Update is called once per frame
	void Update () {
		
		
		rootOfTree.search ();  // search for our choice to do. ie update
		
		
		
	}
	
	
}

