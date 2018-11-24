using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking; 

public class deathPudManager : NetworkBehaviour {

	public GameObject [] deathPudSpawner; 


	public GameObject [] returnPuddleSpawnPoints(){


		return deathPudSpawner;
	}


	public void randomizeLocation(){
		
		int counter = 0;
		while (counter < 100) {
			int randNum = Random.Range(0, 16);
			int randNum2 = Random.Range(0,16); 
			
			
			GameObject holder = deathPudSpawner[randNum];
			deathPudSpawner[randNum] = deathPudSpawner[randNum2]; 
			deathPudSpawner[randNum2] = holder; 
			
			
			counter ++; 
		}


	}

	// Use this for initialization
	void Start () {

		randomizeLocation ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
