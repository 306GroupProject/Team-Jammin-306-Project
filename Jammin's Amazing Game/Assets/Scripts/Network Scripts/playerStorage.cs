using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking; 

// This is older code now, DON'T DELETE IT JUST YET, there might be still a use for this class!
// Concept is to store objects and transfer it between gameobjects to scan for data.
public class playerStorage {
	
	public GameObject[] players; 
	
	
	public playerStorage(int plyerSize){
		
		players = new GameObject[plyerSize];


		
	}
	
	
	public void storePlayers(GameObject playerLoadedIn, int index){


		players [index] = playerLoadedIn; 
	}
	
	
	
	
	public GameObject[] returnPlayers(){
		
		return players; 
	}
}
