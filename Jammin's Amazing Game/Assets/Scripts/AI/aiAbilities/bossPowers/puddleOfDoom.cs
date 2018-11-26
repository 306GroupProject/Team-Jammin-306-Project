using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking; 


public class puddleOfDoom : Abilities {







	//[Command]
	public void CmdCast(Vector2 [] playersPos){


        for (int i = 0; i < this.gameObject.GetComponent<bossAi>().numberWaterPuddlestoSpawn; i++) {


            GameObject newEPuddle = Instantiate(this.projectile, playersPos[i], Quaternion.identity);

            newEPuddle.GetComponent<EPuddleCollison>().isThisBossMan = true;


            Destroy(newEPuddle, 2f);

        }

        this.gameObject.GetComponent<bossAi>().waterPuddlesCasted = false;

    }



	//[ClientRpc]
	//public void RpcCast(Vector2 [] playerPos){

	//	for (int i =0; i < this.gameObject.GetComponent<bossAi>().numberWaterPuddlestoSpawn; i ++) {


	//		GameObject newEPuddle = Instantiate(this.projectile, playerPos[i], Quaternion.identity);

	//		newEPuddle.GetComponent<EPuddleCollison>().isThisBossMan = true; 


	//		Destroy(newEPuddle, 2f); 

	//	}

	//	this.gameObject.GetComponent<bossAi> ().waterPuddlesCasted = false;
			


	//}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

}
