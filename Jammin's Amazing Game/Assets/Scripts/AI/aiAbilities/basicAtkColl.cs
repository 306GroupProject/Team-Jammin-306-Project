using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class basicAtkColl : NetworkBehaviour {
	public GameObject rngAi;
	public int playerSpotted;

	public void giveAi(GameObject rngAI, int spotted){

		this.rngAi = rngAI;
		playerSpotted = spotted; 

	}

	public void OnCollisionEnter2D(Collision2D collision){

		GameObject plyAttacking = rngAi.GetComponent<rangeAi> ().plyController.GetComponent<netWorkAssitant>().playerManager[playerSpotted].ply.gameObject;
	

		if (plyAttacking.name.Equals (collision.gameObject.name)) {

			plyAttacking.GetComponent<playerHealth> ().Damage (rngAi.GetComponent<rangeAi> ().aiDmg);


		} 

		if (!collision.gameObject.tag.Equals ("Enemy")) {


			Destroy (this.gameObject);
		}

	}
}
