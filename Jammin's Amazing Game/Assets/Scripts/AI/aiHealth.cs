using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking; 



public class aiHealth : NetworkBehaviour {


	public const int maxHealth = 16; 
	
	
	[SyncVar]	// keep this variable up to date with the server. 
	public int aiHP = maxHealth; //the players current health. 

	private bool isDead = false;

	public void Damage(int dmg){
		
		if(!isServer){ // this is to ensure that damage is to only be applied on the server.
			return; 
			
		}


        this.GetComponent<Animator>().SetTrigger("Hurt");
		
		aiHP = aiHP - dmg; 
		
		if (aiHP <= 0) {
			
			isDead = true; 
			death(); 
		}
		
	}
	

	public void death(){
		
		if (isDead == true) {
			

			this.GetComponent<ai>().aiMovementSpeed = 0; 

			// We need to put a death animation in here. For future programming.
			
		}
		
		
	}



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (isDead == true) {



			NetworkServer.Destroy(this.gameObject);
		}

		
	}
}
