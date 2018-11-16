using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking; 
using UnityEngine.UI; 


public class playerHealth : NetworkBehaviour {
	
	public const int maxHealth = 16; 
	
	
	[SyncVar(hook = "changeHealth")] public int playerHP = maxHealth; //the players current health.
	
	[SyncVar(hook = "changeIsDead")]private bool isDead = false;
	private float speedStorage;
	private float Timer; 
	private Animator anim;
	
	
	//-------------------[[Hooks]]---------------------------------//
	
	// hooks allow us to attach a function to a vriable it ensures that the values inside the varibles get updated 
	// when something happens over the Network.
	public void changeHealth(int health){
		this.GetComponentsInChildren<Text> () [0].text = this.gameObject.GetComponent<ChangePlayerIdentity> ().playerName + " HP: " + health;
		
	}
	
	
	public void changeIsDead(bool isDead){
		
		this.isDead = isDead; 
	}
	
	
	
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
		
		playerHP = playerHP - dmg; // do the damage against this player.
		
		//if (isLocalPlayer) {
		// update the text on the health.
		//}
		
		
		if (playerHP <= 0) {
			
			isDead = true;  
			Timer = 2f; 
			this.GetComponent<PlayerManager>().speed = 0; 
			print ("your dead! Wait the timer!" + Timer); 
		}
		
	}

    public void Heal(int heal) {
        if (!isServer) {
            return;
        }

        if (playerHP >= 16) {
            return;
        }

        playerHP += heal;
        
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
	
	//[ClientRpc]
	public void respawn(){
		if (!isServer) {
			
			return; 
		}
		if (Timer != 0) {
			
			Timer -= Time.deltaTime;
			
			if (Timer <= 0) {
				
				Timer = 0;
			}
			
		} else { 
			if (isDead == true) {
				
				this.GetComponent<PlayerManager> ().speed = speedStorage;  // restore speed of player.
				
				playerHP = 1; 
				
				
				isDead = false; 
				
				// change animation from dead back to alive!
				
			}
		}
		
		
		
		
	}
	// Use this for initialization
	void Start () {
		
		anim = GetComponent<Animator>();
		
		speedStorage = this.GetComponent<PlayerManager> ().speed; 
		this.GetComponentsInChildren<Text>()[0].text +=   this.gameObject.GetComponent<ChangePlayerIdentity> ().playerName + " Hp: " + playerHP; 
		
	}
	
	
	// Update is called once per frame
	void Update () {
		
		respawn();
		
		
		
	}
}