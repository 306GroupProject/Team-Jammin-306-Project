using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossAi : ai {
	//--------------[[GeneralBoss Instance Variables:]]--------------------//
	public float CoolDownBeforeChange; // the cooldown before the boss AI changes his magic type.
	private float time = 0; 			// the current time that this AI is once the AI is at 0, it is time to change.
	
	//private GameObject prefabOfChoice; // the current basic Attack the the Boss AI wants to use on the players.
	public float spottingDistance;		// the spotting distance that the AI can see players(the bosses will be the size of the entire room.)
	public float waitBeforeRandCast; 
	
	
	
	//--------------------[[FireMode]]------------------------//
	public int numberFireBallsToSpawn;	// how many fire balls do we want to spawn each time it is casted?
	//public GameObject fireBallPrefab; // change the basic attack prefab to this.
	private bool fireBoulderCasted = false; // has our fireboulder been casted yet?
	
	//-----------------[[Water Mode:]]----------------------------------//
	
	[HideInInspector] public int numberWaterPuddlestoSpawn; // detemines how many puddles to spawn randomly. 
	[HideInInspector] public bool waterPuddlesCasted = false;// is this spell currently being casted?
	
	//---------------------[[Rock Mode:]]----------------------------------------------//
	private bool castRockWall = false; 
	private GameObject[] rockWallPosition;
	private float rockWallTimer; 
	
	//------------------------[[Lighting Mode:]]----------------------------------------------------//
	
	[HideInInspector]public bool castAttackSpeed = false; 
	public int cycles; 
	private int newSpeedTimeFired; 
	
	
	
	
	
	
	
	
	
	/**
	 * OnCollisionEnter2D(coll):
	 * 
	 * param:	Coll: a collision that hits this gameobject(boss AI)
	 * 
	 * the collision function players a very big part in this script.
	 * This function determines what hits the AI,for example if  it is a boulder, then this function will tell
	 * the ai to make the decision to fire a meteor at the players. 
	 * 
	 * return: Nothing.
	 * 
	 */ 
	public void OnCollisionEnter2D(Collision2D coll) {
		//print (coll.gameObject.tag + this.gameObject.tag);
		// if the collision is a Boulder, and I am the fire boss, then I will am going to cast meteor at the players!
		if (coll.gameObject.tag.Equals( "Boulder") && this.gameObject.tag.Equals("bossF")) {
			
			fireBoulderCasted = true; 
		}
		
		if (coll.gameObject.tag.Equals ("Fireball") && this.gameObject.tag.Equals ("bossR")) {
			
			castRockWall = true; 
		}
		
		
		
	}
	
	
	//-------------------------------------[[Decisions: Boolean]]-----------------------------------------------//
	
	// propbably will need to change this algorithim for the boss:
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
	 * changePowerLVL():
	 * 
	 * determines if it is time for the Boss AI to change what power it is. 
	 * count down through the AI game timer. if it reaches 0 it is time to swap powers.
	 * 
	 * return: boolean True, if it is time to swap power type, or boolean false if it is not time to swap power type.
	 */ 
	
	public bool changePowerLVL(){
		
		// the in-game timer will be set in the insepector.
		//will be recursivly subtracted until it is time to change powers.
		if (time != 0) {
			
			time -= Time.deltaTime;
			
			
			if(time <= 0){
				time = 0;
				return true; 
			}
			
			return false;
		}
		
		// time to swap. 
		if (time == 0) {
			
			return true; 
		}
		
		return false; 
		
		
	}
	
	/**
	 * NeedToCast():
	 * 
	 * this function asks the AI if it still needs to cast its current spell. Lets
	 * say the the Player casts boulder at the boss AI. It will want to cast it's spell a 
	 * certian number of times. So this function tells the decision tree that it needs to
	 * cast that spell still. 
	 * 
	 * return: boolean true, if still needs to cast its current spell. else boolean false if it is done casting.
	 * 
	 */ 
	public bool NeedToCast(){
		
		// the current spell is firebolder, does it still need to be casted?
		if (fireBoulderCasted == true) {
			
			return true; 
			
		} else if (waterPuddlesCasted == true) {
			
			return true; 
			
		} else if (castRockWall == true){
			
			return true; 
			
		} else if(castAttackSpeed == true){ 
			
			return true; 
			
			
		} else{
			
			return false; 
		}
		
		
	}
	
	
	//----------------------------------------[[Actions: Void]]----------------------------------------------------------------//

	/**
	 * changeType():
	 * 
	 * randomly Changes the boss to a new power type, will cycle through
	 * the main four elements in the game. Once it changes the boss is able to 
	 * cast a new set of counter spells at players. 
	 * 
	 * return: Nothing.
	 * 
	 */ 

	public void changeType(){

		// randomly get a number between 0-3, depending on the number that will change the power type.
		int powerType = Random.Range(0, 4);
		
		if (powerType == 0) {

			// changethe color, and the tag of what type of boss he is now.
			this.gameObject.GetComponent<SpriteRenderer> ().color = Color.red;  
			
			this.gameObject.tag = "bossF"; 
			//prefabOfChoice = fireBallPrefab; 
			
		} else if (powerType == 1) {

			
			this.gameObject.GetComponent<SpriteRenderer> ().color = Color.blue; 
			
			this.gameObject.tag = "bossW";
			
		} else if (powerType == 2) {

			
			this.gameObject.GetComponent<SpriteRenderer>().color = Color.gray; 
			
			this.gameObject.tag = "bossR";
			
		}else if(powerType == 3){

			this.gameObject.GetComponent<SpriteRenderer> ().color = Color.yellow; 
			this.gameObject.tag = "bossL"; 
			
		}
		
		
		// set the time before we can change types again.
		time = CoolDownBeforeChange; 
	}
	
	
	/**
	 * castSpell():
	 * 
	 * this function actually casts the current spell it wishes to cast.
	 * will look through all the boolean spell types and choose which one to cast.
	 * 
	 * return: Nothing void.
	 * 
	 */ 
	public void castSpell(){
		
		
		// check the timer, is it time to cast the next firemeteor?
		if (fireBoulderCasted == true) {
			if (this.GetComponent<fireBoulder> ().attackTime != 0) {
				
				// if not, lets keep subtracting by in game time.
				this.GetComponent<fireBoulder> ().attackTime -= Time.deltaTime; 
				
				if (this.GetComponent<fireBoulder> ().attackTime <= 0) {
					// safety net if statement, meaning that we went below 0, reset it to 0.
					this.GetComponent<fireBoulder> ().attackTime = 0;
				}
				
			}
			
			
			// now that it is time to cast, cast our spell and make sure that it can only be casted as many times as we want to cast it.
			if (this.GetComponent<fireBoulder> ().attackTime == 0 && this.GetComponent<fireBoulder> ().numFireBallSpawned < numberFireBallsToSpawn) {  
				netWorkAssitant players = plyController.GetComponent<netWorkAssitant> ();
				
				for (int i = 0; i < players.playerManager.Count; i ++) {
                    if (players.playerManager[i].ply != null) {
                        Physics2D.IgnoreLayerCollision(this.gameObject.GetComponent<fireBoulder>().returnProjectileLayer(), players.playerManager[i].ply.layer, false);
                    }
                }
				
				
				
				this.GetComponent<fireBoulder> ().CmdCast (this.gameObject.transform.position);
				
				// if we casted all our fire meteors!
			} else if (this.GetComponent<fireBoulder> ().numFireBallSpawned == numberFireBallsToSpawn) {
				
				// reset our counter.
				this.GetComponent<fireBoulder> ().numFireBallSpawned = 0;
				// we have now casted the spell!
				fireBoulderCasted = false; 
				
				// ensure that if a player casts a fire meteor that it does not collide with them.(friendly meteor.)
				int layerChanged = this.gameObject.GetComponent<fireBoulder> ().returnLayerChanged (); 
				
				netWorkAssitant players = plyController.GetComponent<netWorkAssitant> ();
				
				for (int i = 0; i < players.playerManager.Count; i ++) {
					
					Physics2D.IgnoreLayerCollision (layerChanged, players.playerManager [i].ply.layer, true);
				}
				
				
				
			}
			// is it time to cast some death puddles?
		} else if (waterPuddlesCasted == true) {

			// aquire all the spawn points around the map were death puddles can spawn.
			GameObject [] puddleSpawn = GameObject.FindGameObjectWithTag ("deathPudMan").GetComponent<deathPudManager> ().returnPuddleSpawnPoints (); 

			// get the positions of each puddle.
			Vector2 [] pudPos = new Vector2[puddleSpawn.Length]; 
			
			for (int i = 0; i < puddleSpawn.Length; i ++) {
				
				
				Vector2 PPos = new Vector2 (puddleSpawn [i].gameObject.transform.position.x, puddleSpawn [i].gameObject.transform.position.y); 
				
				pudPos [i] = PPos; 
				
			}
			
			 // randomly select how many puddles to spawn.
			numberWaterPuddlestoSpawn = Random.Range (0, 16); 

			// cast the spell. 
			this.gameObject.GetComponent<puddleOfDoom> ().CmdCast (pudPos); 

			
			// re-shuffle which the spawn points in the array.
			GameObject.FindGameObjectWithTag ("deathPudMan").GetComponent<deathPudManager> ().randomizeLocation (); 

		// cast a protective rock wall.
		} else if (castRockWall == true) {

			// check the time before we set the casting of this ability
			// to false. We want to ensure the rock walls are around for a little bit.
			if (rockWallTimer == 0) {
				
				int counter = 0;
				
				while (counter < rockWallPosition.Length) {
					
					
					rockWallPosition [counter].SetActive (true); 
					
					
					counter ++;  
					
				}
				
				rockWallTimer = 3.0f;

				// once we finished casting the, count down until we reach 0 on the timer.
			} else {
				
				if (rockWallTimer != 0) {
					
					rockWallTimer -= Time.deltaTime; 
					
					if (rockWallTimer <= 0) {
						
						rockWallTimer = 0; 
					}
				}

				// once we are at 0, lets hide the rock walls and finish casting this spell .
				if (rockWallTimer == 0) {
					
					int k = 0; 
					while (k < rockWallPosition.Length) {
						
						
						rockWallPosition [k].SetActive (false); 
						
						
						k ++;  
						
					}
					
					castRockWall = false; 
				}
			}
			
			
			
			// change the AI attack speed.
		} else if (castAttackSpeed == true) {

			// how many times have we fired? 
			// we want to make the spell look like it has been increased!
			if (newSpeedTimeFired < cycles) {
				
				
				// wait the timer before attacking the player again.
				if (this.gameObject.GetComponent<basicAttackAI> ().attackTime != 0) {
					
					this.gameObject.GetComponent<basicAttackAI> ().attackTime -= Time.deltaTime;
					
					if (this.gameObject.GetComponent<basicAttackAI> ().attackTime <= 0) {
						
						this.gameObject.GetComponent<basicAttackAI> ().attackTime = 0; 
					}
					
				}
				
				
				// once the cooldown has finished being calculated lets fire a shot at the player.
				if (this.gameObject.GetComponent<basicAttackAI> ().attackTime == 0) {
					
					
					newSpeedTimeFired ++; 
					
					float x = plyController.GetComponent<netWorkAssitant> ().playerManager [playerSpotted].ply.gameObject.transform.position.x;
					float y = plyController.GetComponent<netWorkAssitant> ().playerManager [playerSpotted].ply.gameObject.transform.position.y;
					
					Vector2 myTarget = new Vector2 (x, y);
					Vector2 myPos = this.gameObject.transform.position; 
					
					
					this.gameObject.GetComponent<basicAttackAI> ().CmdCast (myPos, myTarget, true); 
					
					
					
				}
				
				
				
				
			}else {
				// once we are done, set this spell to false, because we are done casting.
				
				castAttackSpeed = false; 
				newSpeedTimeFired = 0; 
			}
			
		}
		
		
		
		
		
		
		
	}

	/**
	 * basicAtk():
	 * 
	 * if the AI is not currently casting spells it needs to be doing something. 
	 * It will be attacking the players if they are within a range. Shoots
	 * a basic attack AI projectle at players.
	 * 
	 * return: Nothing.
	 * 
	 * 
	 * 
	 */ 
	
	public void basicAtk(){
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
			
			this.gameObject.GetComponent<basicAttackAI> ().CmdCast (myPos, myTarget, true); 
		}
		
		
		
	}
	

	/**
	 * returnPlayerSpotted():
	 * 
	 * accessor Function to access which player has been spotted.
	 * 
	 * return: a integer, which player was spotted by the AI? 
	 * 
	 * 
	 */ 
	public int returnPlayerSpotted(){
		
		
		return this.playerSpotted; 
		
	}

	/**
	 * 
	 * CastSpellAtRandom():
	 * 
	 * randomly casts one spell after 6 seconds of wait time. 
	 * If the Ai is not attacking with a basic attack it will cast 
	 * a random spell at the players. 
	 * 
	 * return: Nothing.
	 * 
	 */ 
	public void castSpellAtRandom(){
		
		// wait before we can cast again!
		if (waitBeforeRandCast != 0) {
			
			waitBeforeRandCast -= Time.deltaTime;
			
			if(waitBeforeRandCast <= 0){
				
				waitBeforeRandCast =0;
				
			}
		}
		
		
		// if we can cast:
		if (waitBeforeRandCast == 0) {
			
			// select a spell to cast:
			int powerType = Random.Range (0, 3);

			// select our spell based on our decision!
			if (powerType == 0) {
				
				this.fireBoulderCasted = true;
				
			}
			if (powerType == 1) {
				this.waterPuddlesCasted = true; 
				
			}
			// currently, this will call basic attack which wont work because it has no enemies to fire at.
			// cant randomly call it.
			//if (powerType == 2) {
			//	this.castAttackSpeed = true;

			//}
			if (powerType == 2) {
				this.castRockWall = true; 
				
			}
			
			waitBeforeRandCast = 6.0f; 
		}
	}
	
	
	//---------------------[[buildDecision: ]]----------------------------------//
	
	/**
	 * DecisionTree():
	 *  the decision tree controls and managers this AI.
	 * 
	 * return: nothing.
	 */ 
	public void DecisionTree(){
		
		//--------------[[Decisions to build:]]-------------------//
		decisionTree powerType = new decisionTree ();
		powerType.buildDecision(changePowerLVL); 
		
		decisionTree needCast = new decisionTree (); 
		needCast.buildDecision (NeedToCast); 
		
		decisionTree inRange = new decisionTree ();
		inRange.buildDecision (enemySpottedAndShoot); 
		
		//-------------------[[actions to build:]]---------------------------//
		decisionTree changeType = new decisionTree (); 
		changeType.buildAction (this.changeType);
		
		decisionTree cast = new decisionTree ();
		cast.buildAction (this.castSpell);
		
		decisionTree basicAtck = new decisionTree (); 
		basicAtck.buildAction (this.basicAtk); 
		
		decisionTree castSpellAtRandom = new decisionTree ();
		castSpellAtRandom.buildAction (this.castSpellAtRandom);
		
		
		
		
		
		
		
		//--------[[build the tree: ]]-----------//
		
		powerType.Right (changeType); 
		powerType.Left (needCast);
		
		needCast.Right (cast);
		needCast.Left (inRange);
		
		inRange.Right (basicAtck); // bsicAttack here.
		inRange.Left (castSpellAtRandom);
		
		rootOfTree = powerType; 
	}
	
	// Use this for initialization
	void Start () {
		plyController = GameObject.FindGameObjectWithTag("assistantNet"); 
		
		rockWallPosition = GameObject.FindGameObjectsWithTag("rockWall");

		// set up the rockwall for when the boss fight happens!
		int counter = 0;

		while (counter < rockWallPosition.Length) {
			
			rockWallPosition[counter].SetActive(false);
			
			
			
			counter ++; 
		}
		
		fireBoulderCasted = false; 
		waterPuddlesCasted = false;
		castRockWall = false; 
		castAttackSpeed = false; 
		
		
		
		DecisionTree ();
	}
	
	// Update is called once per frame
	void Update () {
		rootOfTree.search ();
	}
}
