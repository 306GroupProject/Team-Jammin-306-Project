using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class basicAtkColl : NetworkBehaviour {
	public GameObject rngAi;
	public bool isBossman;

	public void giveAi(GameObject rngAI, bool isBossMan){

		this.rngAi = rngAI;
		isBossman = isBossMan;

	}

	public void OnCollisionEnter2D(Collision2D collision){
		

		if (collision.gameObject.tag.Equals("Player1")|| collision.gameObject.tag.Equals("Player2")|| collision.gameObject.tag.Equals("Player3")|| collision.gameObject.tag.Equals("Player4")) {

			if(isBossman == true){

				collision.gameObject.SendMessage ("Damage", rngAi.GetComponent<bossAi>().aiDmg);

			}else{
				collision.gameObject.SendMessage ("Damage", rngAi.GetComponent<rangeAi>().aiDmg);

			}

		} 

		if (collision.gameObject.layer == 16 || !collision.gameObject.tag.Equals("Enemy")) {


			Destroy (this.gameObject);
		}

	}
}
