using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
// "Hey lets make classes that can have structs in them as well :)"

public struct plyStore{		// this is the plyStore structure. I made a data type structure that could hold a Gameobject since none of the default ones could be initlized as a GameObject.
	
	
	public GameObject ply;
	
	public plyStore(GameObject ply){ 	// constructor for the Struct.
		
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



	/**
	 * CmdAddNewPlayer(ply, networkAssitant):
	 * Param: 	ply: GameObject, this is the player GameObject that is stored within the SyncList.
	 * 			netWorkAssitant: this is the class itself, used to access the Structure in the Class.
	 * 
	 * A command that adds the playerGameObject to the SyncList, syncList will keep up-to-date all data stored in it and return info back to all users.
	 * Use it with the build list function, its main purpose is to build within the netWorkAssitant class.
	 * 
	 * return: Nothing
	 * 
	 */ 
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

	public override void OnStartServer ()
	{
		// this is in the case where the host leaves the server, it will refersh the sync list.
		if (playerManager.Count != 0) {
			playerManager.Clear (); 
		}
		

	}

		public override void OnStartClient() {
		
		networkMan = GameObject.FindGameObjectWithTag("networkManager"); // need to get the current PlayerPrefab!
		//isPlayerManagerEmpty = true;

		}



	/**
	 * CmdRemovePlayer(tag):
	 * param:	tag: the tag of the player that is currently leaving the server.
	 * 
	 * 
	 *  Removes the current player that left the server. Will check which player left and remove it so no errors pop up.
	 * 
	 * returns Nothing
	 */ 
	[Command] 
	public void CmdRemovePlayer(string tag){

		int counter = 0;

		while(counter < playerManager.Count){

			if(playerManager[counter].ply.gameObject.tag == tag){

				print ("Found player, removing the following index:" + counter); 
				playerManager.RemoveAt(counter);

			}else{

				print ("searching for player!");
			}

			counter ++; 
		}

	}

	/**
	 * deleteOldPlayers(tag)
	 * 
	 * param:	Tag: String, feed this string into cmdRemovePlayer
	 * 
	 * function which accesses CmdRemovePlayer.
	 * 
	 * returns: Nothing
	 * 
	 */ 

	public void deleteOldPlayers(string tag){
		if (!isServer) {

			return;
		}
		CmdRemovePlayer (tag);

	}
 

	/**
	 * deleteOldPlayers(newPlayer)
	 * 
	 * param:	newPlayer: The gameObject that is wanted to be stored.
	 * 
	 * Function which accesses, CmdAddNewPlayer.
	 * 
	 * returns: Nothing
	 * 
	 */ 

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
			oldObject = currentObject; 
			buildList(oldObject);

		} else if (!oldObject.Equals (currentObject)) { // once we have the first prefab lets test against it until a new prefab is spawned in.

			oldObject = currentObject; // ideally this will be the second player :)

			buildList(oldObject);  
		}
		
		
	}
}
