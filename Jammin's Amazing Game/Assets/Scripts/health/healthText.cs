using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.Networking; 


public class healthText : NetworkBehaviour {




	// Use this for initialization
	void Start () {



	}


	// Update is called once per frame
	void LateUpdate () { 

		Vector2 screenPos = Camera.main.WorldToScreenPoint (this.transform.position);  // find position of player in world.

		screenPos.y += 30;  // move it up and to the center of the players head.
		screenPos.x += 30; 
	

		this.GetComponentInChildren<Text> ().transform.position = screenPos;  // set the Texts position to the screen position.




	}
}
