using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

// first spell combo that can be used on players.

public class fireBoulder : Abilities {
	
	public float attackTime;  // the attackTime, when can we cast our spell again?
	public int numFireBallSpawned = 0; // how many fire balls need to be spawned.
	private Vector2[] position = {Vector2.left, Vector2.right, Vector2.up, Vector2.down}; // which way to shoot our fire balls. 
	private int layerChanged = -1; // which layer did we change when damaging players.

	/**
	 * CmdCast(bosspos, playerSpotted):
	 * param:	Vector2 BossPos, this is the boss position.
	 * 			Vector2 playerSPotted, this the player that is spell is targeted towards
	 * 
	 * the current build of this function shoots the fire balls four different ways, eventually we want to
	 * make the fire balls shoot in a spray pattern. cmdCast updates the server, puts data into
	 * RPCcast, so clients are updated.
	 * 
	 * return: void.
	 * 
	 * 
	 */ 
	[Command]
	public void CmdCast(Vector2 bossPos){
		
		
		this.RpcCast (bossPos);
		
		
	}

	/**
	 * RpcCast(bosspos, playerSpotted):
	 * param:	Vector2 BossPos, this is the boss position.
	 * 			Vector2 playerSPotted, this the player that is spell is targeted towards
	 * updates the player of whats happening on the screen. Casts four different meteors 
	 * in four different directions. 
	 * 
	 * return: Nothing void.
	 */
	
	[ClientRpc]
	public void RpcCast(Vector2 bossPos){
	

		// turn of collision of meteor and players, we need to gather all the players currently in the game.
//		netWorkAssitant players = this.gameObject.GetComponent<bossAi> ().plyController.GetComponent<netWorkAssitant> ();



		for (int i =0; i < position.Length; i ++) {
			
			// create a fire meteor.	
			GameObject spawnFB = Instantiate(this.projectile, bossPos, Quaternion.identity); 
			
			spawnFB.GetComponent<Meteor> ().isThisBossMan = true; 
	
			// turn of collision with its self, so the meteor can actually travel through its self.
			Physics2D.IgnoreCollision(spawnFB.GetComponent<CircleCollider2D>(), this.gameObject.GetComponent<BoxCollider2D>(), true); 

			//for(int j = 0; j < this.gameObject.GetComponent<bossAi>().plyController.GetComponent<netWorkAssitant>().playerManager.Count; j++){
				// ensure that the player can be hit by this meteor that is spawned by the AI.

			//	Physics2D.IgnoreLayerCollision(spawnFB.layer, this.gameObject.GetComponent<bossAi>().plyController.GetComponent<netWorkAssitant>().playerManager[j].ply.layer, false);
			//}

			// store which layer was changed.
			layerChanged = spawnFB.layer; 

			// spawn.
			spawnFB.GetComponent<Rigidbody2D>().AddForce(position[i] * velocity);



			
			
			
		}


		
		attackTime = cooldown; 
		
		numFireBallSpawned ++; 
		
		
		
	}


	/**
	 * returnLayerChanged():
	 * 
	 * accessor function, returns the layer that was changed so we can revert for when 
	 * players cast meteor.
	 * 
	 * return: nothing void. 
	 * 
	 */ 

	public int returnLayerChanged(){


		return layerChanged; 
	}


	public int returnProjectileLayer(){


		return this.projectile.layer; 
	}
	
	// Use this for initialization
	void Start () {
		
		
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
