using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking; 
using UnityEngine.UI; 


public class playerHealth : NetworkBehaviour {

	public const int maxHealth = 16; 


	[SyncVar]	// keep this variable up to date with the server. 
	public int playerHP = maxHealth; //the players current health. 

	private GameObject playerSpawnPoint;
	private bool isDead = false;
	private float speedStorage;

    private Animator anim;
	
	
	
	public void Damage(int dmg){
		
		if(!isServer){ // this is to ensure that damage is to only be applied on the server.
			return; 
			
		}

        anim.SetTrigger("Hurt"); // play the hurt animation when the player is damaged

		playerHP = playerHP - dmg; 
		this.GetComponentsInChildren<Text> () [0].text = "Health: " + playerHP; 
		
		if (playerHP <= 0) {

			isDead = true; 
			death(); 
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
			
			this.GetComponent<CharacterController>().speed = 0; 
			print ("you are dead, hit 'R' to respawn..."); 

			// We need to put a death animation in here. For future programming.
				
		}

		
	}


	public void respawn(){

		if (isDead == true) {

			if(Input.GetKeyDown(KeyCode.R)){
				playerSpawnPoint = GameObject.FindWithTag("Respawn");
				
				playerHP = maxHealth; 	// set the players Health back to Max.
                this.GetComponentsInChildren<Text>()[0].text = "Health: " + playerHP; // set the text to display the player's new health total (should be at max)

                this.GetComponent<CharacterController>().speed = speedStorage;  // restore speed of player.
				
				this.transform.position = playerSpawnPoint.transform.position;  // place player at spawn point. 
				
				isDead = false; 
				
			}

		
		}
	
	}
	// Use this for initialization
	void Start () {

        anim = GetComponent<Animator>();

		speedStorage = this.GetComponent<CharacterController> ().speed; 
		this.GetComponentsInChildren<Text>()[0].text += " " + playerHP; 

	}
	
	// Update is called once per frame
	void Update () {

		respawn (); 

	}
}
