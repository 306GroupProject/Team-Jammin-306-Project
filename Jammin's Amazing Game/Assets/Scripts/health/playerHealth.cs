using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


public class playerHealth : NetworkBehaviour {

    public const int maxHealth = 16;


    [SyncVar(hook = "changeHealth")] public int playerHP = maxHealth; //the players current health.

    [SyncVar(hook = "changeIsDead")] private bool isDead = false;
    private float speedStorage;
    private float Timer;
    private Animator anim;


    //-------------------[[Hooks]]---------------------------------//

    // hooks allow us to attach a function to a vriable it ensures that the values inside the varibles get updated 
    // when something happens over the Network.
    public void changeHealth(int health) {
        this.GetComponentsInChildren<Text>()[0].text = this.gameObject.GetComponent<ChangePlayerIdentity>().playerName + " HP: " + health;

    }


    public void changeIsDead(bool isDead) {

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


    public void Damage(int dmg) {

        if (!isServer) { // this is to ensure that damage is to only be applied on the server.
			return; 
        }

        anim.SetTrigger("Hurt"); // play the hurt animation when the player is damaged

        playerHP = playerHP - dmg; // do the damage against this player.


        if (playerHP <= 0) {

            isDead = true;
            Timer = 3f;
            playerHP = 0;
			this.gameObject.GetComponent<PlayerManager>().speed = 0; 

			CmdTurnOff(); 
        }

    }


	/**
	 * CmdTurnOff(): 
	 * 
	 * server calls this command to update our clients that they cant move anymore!
	 * 
	 * return: Nothing.
	 */ 
	[Command]
	public void CmdTurnOff(){

		RpcTurnOff (); 

	}

	
	/**
	 * RpcTurnOn():
	 * 
	 * Client gets updated when player dies turning off all old scripts. 
	 * 
	 * return: Nothing
	 */

	[ClientRpc]
	public void RpcTurnOff(){
		

		int counter = 0; 
		// we want to ensure that players cannot attack or teleport!
		// so lets turn off all abilites and teleport so they cant move or attack!
		// aquire all components on this gameObject.
		while(counter < this.gameObject.GetComponents(typeof(Component)).Length){
					
			//print( "Length of playerArray:"+ this.gameObject.GetComponents(typeof(Component)).Length); 
					
			// count through all componts, if any of them ahve a subClass of Abilites, then lets turn it off.
			if(this.gameObject.GetComponents(typeof(Component))[counter].GetType ().IsSubclassOf(typeof(Abilities))){
						
				Abilities compToTurnOff = (Abilities)this.gameObject.GetComponents(typeof(Component))[counter];
						
				compToTurnOff.enabled = false; 
						
			// else, if the component is equal to Teleport then lets turn it off. 
			}else if (this.gameObject.GetComponents(typeof(Component))[counter].Equals(this.gameObject.GetComponent<Teleport>())){
						
						
				Teleport compToTurnOff = (Teleport)this.gameObject.GetComponents(typeof(Component))[counter];
						
				compToTurnOff.enabled = false; 
						
			}
					
					
			//this.gameObject.GetComponents(typeof(Abilities))[counter].gameObject.name;  
					
			counter ++;
					
		}
	}

	/**
	 * CmdTurnOn(): 
	 * 
	 * server calls this command to update our clients that they can move now!
	 * 
	 * return: Nothing.
	 */
	[Command]
	public void CmdTurnOn(){
		RpcTurnOn (); 
	}


	/**
	 * RpcTurnOn():
	 * 
	 * Client gets updated when player respawns turning on all old scripts. 
	 * 
	 * return: Nothing
	 */
	[ClientRpc]
	public void RpcTurnOn(){ 

		int counter = 0; 
		
		// now that powers are off, lets turn them back on once the player "respawns"
		while(counter < this.gameObject.GetComponents(typeof(Component)).Length){
			
			//print( "Length of playerArray:"+ this.gameObject.GetComponents(typeof(Component)).Length); 
			
			// count through all componts, if any of them ahve a subClass of Abilites, then lets turn it on.
			if(this.gameObject.GetComponents(typeof(Component))[counter].GetType ().IsSubclassOf(typeof(Abilities))){
				
				Abilities compToTurnOff = (Abilities)this.gameObject.GetComponents(typeof(Component))[counter];
				compToTurnOff.enabled = true; 
				
				// else, if the component is equal to Teleport then lets turn it on. 
			}else if (this.gameObject.GetComponents(typeof(Component))[counter].Equals(this.gameObject.GetComponent<Teleport>())){
				
				
				Teleport compToTurnOff = (Teleport)this.gameObject.GetComponents(typeof(Component))[counter];
				
				compToTurnOff.enabled = true; 
				
			}
			
			
			//this.gameObject.GetComponents(typeof(Abilities))[counter].gameObject.name;  
			
			counter ++;
			
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
    public void respawn() {
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

                this.GetComponent<PlayerManager>().speed = speedStorage;  // restore speed of player.

                playerHP = 5;


                isDead = false;

				this.CmdTurnOn(); 
                // change animation from dead back to alive!

            }
        }




    }
    // Use this for initialization
    void Start() {

        anim = GetComponent<Animator>();

        speedStorage = this.GetComponent<PlayerManager>().speed;
        this.GetComponentsInChildren<Text>()[0].text += this.gameObject.GetComponent<ChangePlayerIdentity>().playerName + " Hp: " + playerHP;
    }


    // Update is called once per frame
    void Update() {

        respawn();



    }
}