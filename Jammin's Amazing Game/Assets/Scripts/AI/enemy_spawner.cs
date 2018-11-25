using System.Collections;
using UnityEngine.Networking; 
using System.Collections.Generic;
using UnityEngine;

public class enemy_spawner : NetworkBehaviour {
	
	public GameObject meleePrefab; // Select the AI prefab you want drag and drop it into this variable in unity.
	public GameObject rangePrefab; // select the Range AI prefab.
	public GameObject bossPrefab; // boss to spawn
	
	public GameObject bossSpawnPoint; 
	public GameObject[] MeleePoints; // spawn points
	public GameObject[] RangePoints; // spawn Points
	
	public int numMeleeAi; // how many AI do we want to spawn? make sure it is the same size as the spawnPoints[].
	public int numRangeAi;
	public int bossAI;
	
	/**
	 * Spawns a enemy at the position on the map that you put down IE game object. 
	 * This will be later added on I am thinking a List of gameobjects which are placed around 
	 * the map, this will allow for a quick spawning of enemies. 
	 * 
	 */ 
	public override void OnStartServer(){
		int counter = 0; 
		if (numMeleeAi != 0) {
			while (counter < numMeleeAi) {
				
				GameObject enemy = Instantiate (meleePrefab, new Vector2 (MeleePoints [counter].transform.position.x, MeleePoints [counter].transform.position.y), Quaternion.identity);
                enemy.transform.parent = GameObject.Find("melee").gameObject.transform;
                NetworkServer.Spawn (enemy); 
				
				
				counter ++;
			} 
		}
		
		if (numRangeAi != 0) {
			
			counter = 0; 
			
			while (counter <  numRangeAi) {
				
				GameObject rngEnemy = Instantiate (rangePrefab, new Vector2 (RangePoints [counter].transform.position.x, RangePoints [counter].transform.position.y), Quaternion.identity);
                rngEnemy.transform.parent = GameObject.Find("ranged").gameObject.transform;
				NetworkServer.Spawn (rngEnemy); 
				
				counter ++; 
				
				
			}
		}
		
		if (bossAI != 0) {
			counter = 0;
			
			while (counter < bossAI) {
				
				GameObject boss = Instantiate (bossPrefab, new Vector2 (bossSpawnPoint.transform.position.x, bossSpawnPoint.transform.position.y), Quaternion.identity);
                NetworkServer.Spawn (boss);
				
				counter ++; 
			}
			
		}
		
	}
	
	
	void Start(){
	}
	
}