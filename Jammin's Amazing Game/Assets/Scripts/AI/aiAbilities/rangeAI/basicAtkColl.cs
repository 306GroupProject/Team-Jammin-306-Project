using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class basicAtkColl : NetworkBehaviour {
	public GameObject rngAi;
	public int playerSpotted;
	public bool isBossman;

	public void giveAi(GameObject rngAI, int spotted, bool isBossMan){

		this.rngAi = rngAI;
		playerSpotted = spotted;
		isBossman = isBossMan;

	}

	public void OnCollisionEnter2D(Collision2D collision){

		GameObject plyAttacking = GameObject.FindGameObjectWithTag("assistantNet").GetComponent<netWorkAssitant>().playerManager[playerSpotted].ply.gameObject;


		if (plyAttacking.name.Equals (collision.gameObject.name)) {

			if(isBossman == true){

				plyAttacking.GetComponent<playerHealth> ().Damage (rngAi.GetComponent<bossAi> ().aiDmg);

			}else{

				plyAttacking.GetComponent<playerHealth> ().Damage (rngAi.GetComponent<rangeAi> ().aiDmg);
			}

		} 

		if (!collision.gameObject.tag.Equals ("Enemy")) {


			Destroy (this.gameObject);
		}

	}
}
