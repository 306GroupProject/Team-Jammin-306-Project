using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking; 
using UnityEngine;

public class ai : NetworkBehaviour {
	[SerializeField]
	private GameObject [] playerPosition; 
	private int playerSpotted = -1; 
	private float aiSavedSpeed; 
	private Animator anim;

	[SerializeField]
	private playerStorage playerstore; 

	private int plyerDmg; 
	//----------------------[[private and public]]-----------------------------------------------------//
	
	public float aiMovementSpeed = 0;  // aiMovement will need to be set in unity. 
	public int aiDmg = 0; 		// aiDmg, will need to be set in unity
	public decisionTree rootOfTree;  // reference to Decision Tree, which points at root.
	public float timer = 0; 
	
	
	
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
	
	
	/**
	 * initPlayerPos(): 
	 * Initializes all the current players in the game. This allows the AI to be able to make decisions based
	 * on how many players are on the current Scene. This would need to be called regually maybe every other second
	 * just so we can keep up-to-date on all the players. Some might join, some might leave etc.
	 * 
	 * returns: Nothing
	 * 
	 */ 
	public void initPlayerPos(){
		if (this.playerstore.players.Length == 0) {
			
			return;
			
		} else {
			
			this.playerPosition = this.playerstore.returnPlayers(); 
			timer = 2f;
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
		
		// is enemySpotted this is our first decision!
		
		if (playerPosition [0] != null) { 		// check to see if this player is active.
			// if so then lets get their distance.
			if (Vector2.Distance (this.transform.position, playerPosition [0].transform.position) < 10f) {
				
				anim.SetBool ("IsMoving", true); // return true if player is spotted, store in a variable which player is spotted.
				playerSpotted = 0; 
				
				return true;
				
			} else {
				// else this player was not spotted, return false. All players below are setup same way as this one.
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
		aiMovementSpeed = aiSavedSpeed; 
		// if the Vector2 position is less then 7f distance, we will apply force to move towards Player. 
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
		
		this.aiMovementSpeed = 0f; 
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
		//	rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		GameObject networkManager  =  GameObject.FindGameObjectWithTag("networkManager");
		playerstore = networkManager.GetComponent<CustomNetwork> ().returnPlayers (); 
		
		aiSavedSpeed = aiMovementSpeed; 
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (timer != 0) {
			
			timer -= Time.deltaTime;
			
			if (timer <= 0) {
				
				timer = 0; 
			}
		}
		
		
		if (timer == 0) {
			
			this.initPlayerPos (); 
			
		}
		
		
		
		rootOfTree.search ();  // search for our choice to do. ie update
		
		
		
	}
	
	
}
