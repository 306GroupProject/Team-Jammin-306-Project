using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking; 

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
