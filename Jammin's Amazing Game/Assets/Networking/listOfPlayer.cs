using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking; 
using UnityEngine.UI; 


// A list of all the players currently in the scene. This will help with decieding which player to attack.
public class listOfPlayer : NetworkBehaviour {


	private LinkedList<GameObject> players; // a Linked list containing all the players in the game.

	public override void OnStartServer(){
		print ("starting up server!"); 

		this.players = new LinkedList<GameObject> ();

		players.AddLast(GameObject.FindGameObjectWithTag("Player"));
		//int count = players.Count; 
		//this.GetComponentsInChildren<Text> () [1].text = "Health: " + count ;

	}


	/*
	public override void OnStartLocalPlayer(){


		players.AddLast (this.gameObject);
		int count = players.Count; 
		this.GetComponentsInChildren<Text> () [1].text += "yeet: " + count ;


	}

 */ 
	// Use this for initialization
	void Start () {


		
	}
	
	// Update is called once per frame
	void Update () {
		print (players.Count);
	}
}
