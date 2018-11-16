using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// range AI, some one needs to control all these chickens!
public class rangeAi : ai {

	public float spottingDistance; // this variable contains how far away the player needs to be before the AI can spot that player.
	public float comfortZone;	// is the player to close to the AI? this is the ais comfort zone before it needs to run away.

//-----------------[[Decisions: Boolean]]------------------//


	/**
	 * enemySpottedAndShoot():
	 * 
	 * seaches all game states, single,duo,trio,quad player to determine if the player is within line of sight to the AI.
	 * if it is, then the AI will make its mind up of which player to persue. 
	 * 
	 * return: Bool, returns true if player has been spotted, returns false if player has not been spotted.
	 * 
	 */ 
	public bool enemySpottedAndShoot(){
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


	/**
	 * plyToClose():
	 * 
	 * if the player gets to close to the AI, the AI will make the choice to run away from the player.
	 * 
	 * return: bool, true if the player is to close, false if the player is faraway enough.
	 * 
	 */ 
	public bool plyToClose(){

		// aquire the player that has been spotted, is this player to close to the AI?
		GameObject ply = plyController.GetComponent<netWorkAssitant> ().playerManager [playerSpotted].ply.gameObject; 

		if (Vector2.Distance (this.transform.position, ply.transform.position) < comfortZone) {

			return true; // return true if it is.

		} else {
			return false;  // else return false if they are not.
		}


	}

//-------------------[[actions:Void]]------------------------------------//

	/**
	 * Attack():
	 * 
	 * If the AI can attack, then it will attack, after its timer.
	 * Calls a special network command built in a seperate file to build and shoot the magic at the player
	 * spotted.
	 * 
	 * return: Nothing void.
	 * 
	 */ 
	public void Attack(){
		// wait the timer before attacking the player again.
		if (this.gameObject.GetComponent<basicAttackAI> ().attackTime != 0) {

			this.gameObject.GetComponent<basicAttackAI> ().attackTime -= Time.deltaTime;

			if(this.gameObject.GetComponent<basicAttackAI> ().attackTime <= 0){

				this.gameObject.GetComponent<basicAttackAI> ().attackTime = 0; 
			}

		}

		// once the cooldown has finished being calculated lets fire a shot at the player.
		if (this.gameObject.GetComponent<basicAttackAI> ().attackTime == 0) {

			float x = plyController.GetComponent<netWorkAssitant> ().playerManager [playerSpotted].ply.gameObject.transform.position.x;
			float y = plyController.GetComponent<netWorkAssitant> ().playerManager [playerSpotted].ply.gameObject.transform.position.y;

			Vector2 myTarget = new Vector2(x, y);
			Vector2 myPos = this.gameObject.transform.position; 
			this.gameObject.GetComponent<basicAttackAI> ().CmdCast (myPos, myTarget); 
		}

	}
	/**
	 * move():
	 * 
	 * Moves the enemy AI away from the player, the rangeWizard AI perfers to attack from a distance rather then close and personal.
	 * 
	 * return: Nothing.
	 * 
	 */ 

	public void Move(){

		if (this.playerSpotted != -1) { // ensure a player was spotted by the decision tree.

			// aquire which player to run away from!
			GameObject ply = plyController.GetComponent<netWorkAssitant> ().playerManager [playerSpotted].ply.gameObject;
			this.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None; // enable chicken to run.
			this.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation; // ensure they can't rotate on the Z axis.

			// if AI was idle, ensure to run.
			aiMovementSpeed = aiSavedSpeed;

			// the direction we want to move to avoid the player chasing the AI.
			Vector2 moveDirection = this.gameObject.transform.position - ply.gameObject.transform.position;

			// move away!
			transform.Translate(moveDirection.normalized * aiMovementSpeed * Time.deltaTime);
            anim.SetBool("IsMoving", true);
        }

	}
	/**
	 * void idle(): 
	 * 
	 * If player is not within line of sight then idle.
	 * 
	 * returns: Nothing
	 */
	public void idle(){
		if (isServer) {
			this.aiMovementSpeed = 0f;

			this.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll; 
			anim.SetBool("IsMoving", false);
		}
	}




//---------------------[[buildDecision: ]]----------------------------------//

	/**
	 * DecisionTree():
	 * 
	 * builds the choice that this AI has when certian events happen in game.
	 * the AI can choose to stay still if no enemy is near by. else if there is check to see if
	 * that player is to close to the AI, if so Move away. Else attack the player.
	 * 
	 * return: nothing.
	 */ 
	public void DecisionTree(){

		decisionTree inRange = new decisionTree();
		inRange.buildDecision (enemySpottedAndShoot);

		decisionTree ply2Close = new decisionTree (); 
		ply2Close.buildDecision (plyToClose); 
		
		decisionTree attack = new decisionTree ();
		attack.buildAction (Attack);

		decisionTree move = new decisionTree ();
		move.buildAction (Move);

		decisionTree idle = new decisionTree (); 
		idle.buildAction (this.idle); 



//--------[[build the tree: ]]-----------//

		inRange.Right(ply2Close);
		ply2Close.Right (move);
		ply2Close.Left (attack); 
		inRange.Left (idle);


		this.rootOfTree = inRange; 
	}


	/**
	 * returnPlayerSpotted:
	 * 
	 * returns which player was spotted, so AI can deteremine who to hit.
	 * 
	 * return: a Integer number, resembling the index in the playerManager sync list.
	 * 
	 */ 
	
	public int returnPlayerSpotted(){
		
		return playerSpotted;
	}


	// Use this for initialization
	void Start () {

        anim = GetComponent<Animator>();
        plyerDmg = 6; 
		plyController = GameObject.FindGameObjectWithTag("assistantNet");
		aiSavedSpeed = aiMovementSpeed; 
		DecisionTree (); 

	}
	
	// Update is called once per frame
	void Update () {
	
		rootOfTree.search (); 
	}
}
