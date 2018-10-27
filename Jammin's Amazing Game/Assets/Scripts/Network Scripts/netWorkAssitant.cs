using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
// "Hey lets make classes that can have structs in them as well :)"

public struct plyStore{												// this is the plyStore structure. I made a data type structure that could hold a Gameobject since none of the default ones could be initlized as a GameObject.
	
	
	public GameObject ply;
	
	public plyStore(GameObject ply){ 								// constructor for the Struct.
		
		this.ply = ply; 
		
	}
	
}

public class synclistplystore : SyncListStruct<plyStore>{} 

																	// this class manages the data for when players join the server, it stores each player that joins into a SyncList. It is like a syncVar accept for holding bigger amounts of data.
public class netWorkAssitant : NetworkBehaviour {
	public synclistplystore playerManager = new synclistplystore(); // this list will keep all current players updated. Throughout the server.
	[SerializeField] private GameObject currentObject = null; 		// This object scans CustomNetwork script mark made, it will grab the prefab from that and store it into this current object. Once a new one comes this is removed.
	[SerializeField] private GameObject oldObject = null;
	//private bool isPlayerManagerEmpty;  							// possibly for later!
	private GameObject networkMan; 									// used to grab the current perfab from CustomNetwork.


																	// I created a command to add players, ANY OTHER WAY will not work like how you expect. You need to do this or your SyncList will littearlly delete objects in the network manager.
	[Command]
	public void CmdAddNewPlayer(GameObject ply, GameObject netWorkAssitant){

		netWorkAssitant assitant = netWorkAssitant.GetComponent<netWorkAssitant> (); 	// create a new network assitant ONLY way to access our structure.
		
		if (assitant != null) { 
			
			assitant.playerManager.Add(new plyStore(ply));  							// add the player into the class structure.

			/**
			int counter = 0;

			// testing, this prints out when ever someone Joins the server basically. 
			while(counter < assitant.playerManager.Count){


				print (assitant.playerManager[counter].ply.gameObject.tag);

				counter ++;
			}
			**/ 
			//isPlayerManagerEmpty = false; I will use this later I just dont want a Warning in console!

		}
		
		
	}

		public override void OnStartClient() {

		networkMan = GameObject.FindGameObjectWithTag("networkManager"); // need to get the current PlayerPrefab!
		//isPlayerManagerEmpty = true;

		}

	/* // This command will be added in later, once a player leaves, we will need to delete that player from the list to ensure no bugs come up!
	[Command] 
	public void CmdRemovePlayer(GameObject ply){



	for(int counter = this.playerManager.Count - 1; counter >= 0; counter --){

		if(this.playerManager[counter].ply == null){

				this.playerManager.rEM
			}
		}


	}
*/ 
	// build our current list
	public void buildList(GameObject newPlayer){


		CmdAddNewPlayer(newPlayer, this.gameObject);

	}

	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {


		this.currentObject = networkMan.GetComponent<CustomNetwork> ().returnCurrentPrefab ();  // check the currentObject that is in the customScript network!

		if (oldObject == null && currentObject != null) { // if this is the first prefab, then lets add it into the first cell.
			print ("Frist place to vist!");
			oldObject = currentObject; 
			buildList(oldObject);

		} else if (!oldObject.Equals (currentObject)) { // once we have the first prefab lets test against it until a new prefab is spawned in.

			oldObject = currentObject; // ideally this will be the second player :)
			print ("New player Joining!");
			buildList(oldObject);  
		}
		
		
	}
}
