using System.Collections;
using UnityEngine.Networking; 
using System.Collections.Generic;
using UnityEngine;

public class enemy_spawner : NetworkBehaviour {
	
	public GameObject aiPrefab; // Select the AI prefab you want drag and drop it into this variable in unity..
	public GameObject[] spawnPoints; // spawn points
	public int numAI; // how many AI do we want to spawn? make sure it is the same size as the spawnPoints[].
	


	/**
	 * Spawns a enemy at the position on the map that you put down IE game object. 
	 * This will be later added on I am thinking a List of gameobjects which are placed around 
	 * the map, this will allow for a quick spawning of enemies. 
	 * 
	 */ 
	public override void OnStartServer(){
		int counter = 0; 

		while (counter < numAI) {

			GameObject enemy = Instantiate (aiPrefab, new Vector2(spawnPoints[counter].transform.position.x, spawnPoints[counter].transform.position.y), Quaternion.identity );
			
			NetworkServer.Spawn (enemy); 
			counter ++;
		} 
		
		
	}


	void Start(){
		
	}
	
}