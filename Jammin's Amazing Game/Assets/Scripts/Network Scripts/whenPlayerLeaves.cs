using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking; 

public class whenPlayerLeaves : NetworkBehaviour {

	private GameObject networkAssit; 



	void OnDisable(){
		

		networkAssit.GetComponent<netWorkAssitant> ().deleteOldPlayers (this.gameObject.tag);

		
	}


	// Use this for initialization
	void Start () {

		networkAssit = GameObject.FindGameObjectWithTag ("assistantNet");

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
