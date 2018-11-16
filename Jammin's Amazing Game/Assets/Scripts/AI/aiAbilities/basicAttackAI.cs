using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking; 

// The spells that can be casted by Range AI. This spell is known as a Basic Attack.

public class basicAttackAI : Abilities {

	public float attackTime; // time inbetween each attack. 



	/**
	 * cmdCast(myPos, playerSpotted):
	 * 
	 * param:	myPos: Vector2, is the vector position of the AI.
	 * 			playerSpotted:Vector2, is the vector position of the player the AI is attacking.
	 * 
	 * a networking command, which tells the server that this AI is currently casting a new 
	 * spell.
	 * 
	 * return: nothing.
	 */ 
	[Command]
	public void CmdCast(Vector2 myPos, Vector2 playerSpotted){

		this.RpcCast (myPos, playerSpotted); 
	}


	/**
	 * RpcCast(myPos, playerSpotted):
	 * 
	 * param:	 myPos: Vector2, is the vector position of the AI.
	 * 			playerSpotted:Vector2, is the vector position of the player the AI is attacking.
	 * 
	 * RPC cast ensures that players on the network can see the attack that is being casted by this AI. 
	 * the AI will build a spell and shoot it towards the player.
	 * 
	 * return: Nothing.
	 * 
	 */
	[ClientRpc]
	public void RpcCast(Vector2 myPos, Vector2 playerSpotted){

		Vector2 direction = (playerSpotted - myPos).normalized;	// the direction in which the basic attack was shoot.
		GameObject spell = Instantiate (this.projectile, myPos, Quaternion.identity); // build the spell. 

		// ensure that the spell does not get, stuck or clump up on the AI it's self.
		Physics2D.IgnoreCollision (spell.GetComponent<CircleCollider2D> (), this.gameObject.GetComponent<CircleCollider2D> (), true);
	
		// add the damage to the spell.
		basicAtkColl coll = spell.AddComponent<basicAtkColl> ();

		coll.giveAi (this.gameObject, this.gameObject.GetComponent<rangeAi>().returnPlayerSpotted());

		// give the spell movement.
		spell.GetComponent<Rigidbody2D> ().AddForce ( direction * velocity);

		// destory spell once job is finished.
		Destroy (spell, 1f);

		attackTime = cooldown; 

	}


}
