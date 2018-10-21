using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking; 
using UnityEngine.UI; 


public class playerHealth : NetworkBehaviour {

	public const int maxHealth = 16; 


	[SyncVar]	// keep this variable up to date with the server. 
	public int playerHP = maxHealth; //the players current health. 
	public GameObject textHealth; 	// the text field for the players Health. 

	private bool isDead = false;
	private float speedStorage;
    private Animator anim;
	
	
	/**
	 * Damage(dmg): 
	 * 
	 * param: dmg: is a integer number to resemble how much health has been lost.
	 * 
	 * function calculates the damage that has been currently done to this player. If the player health drops to 0 it will die.
	 * 
	 * returns: Nothing
	 */ 
	public void Damage(int dmg){
		
		if(!isServer){ // this is to ensure that damage is to only be applied on the server.
			return; 
			
		}


		
        anim.SetTrigger("Hurt"); // play the hurt animation when the player is damaged

		playerHP = playerHP - dmg; 
		this.GetComponentsInChildren<Text> () [0].text = this.gameObject.name +" HP: " + playerHP; // update the health bar/Text when damage is done.
		
		if (playerHP <= 0) {

			isDead = true;  
			death();  // if dead then death function is called.
		}
		
	}
	
	/**
	 * death(): basic death script will set the players movement speed to 0. We will need a death animation later as well.
	 * For now player can respawn by hitting "R", Once we get spells, we can change it to use healing spells.
	 * 
	 * param: None
	 * return: None
	 */
	public void death(){
		
		if (isDead == true) {
			
			this.GetComponent<PlayerManager>().speed = 0; 
			print ("you are dead, hit 'R' to respawn..."); 

			// We need to put a death animation in here. For future programming.
				
		}

		
	}


	/**
	 * respawn():
	 * 
	 * the way we want to do death is as follows: if a player dies, then they can wait until room is cleared at, 
	 * which point the players can respawn and have 1 hp and movement speed back.
	 * 
	 * returns: Nothing
	 * 
	 * 
	 */ 
	public void respawn(){

		if (isDead == true) {

			if(Input.GetKeyDown(KeyCode.R)){
				playerHP = 1; // give them a little bit of health and restore speed
				this.GetComponent<PlayerManager>().speed = speedStorage;  // restore speed of player.
				this.GetComponentsInChildren<Text>()[0].text = this.gameObject.name + " Hp: "  + playerHP; 
				// change animation from dead back to alive!
			}

		
		}
	
	}
	// Use this for initialization
	void Start () {

        anim = GetComponent<Animator>();

		speedStorage = this.GetComponent<PlayerManager> ().speed; 
		this.GetComponentsInChildren<Text>()[0].text +=  this.gameObject.name + " Hp: " + playerHP; 
		
	}


	// Update is called once per frame
	void Update () {
		
		respawn (); 

	}
}
