using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WinKey : NetworkBehaviour {

    [SerializeField]
    private int winKeyID; //use ID for distinguishing which room to jump to next

    /*
     * Center the camera into the first room once the game has started.  
     */
	void Start () {
        GameObject.Find("Main Camera").GetComponent<Camera>().enabled = true;
        GameObject.Find("Main Camera").GetComponent<Camera>().transform.position = GameObject.Find("Room1 Center").transform.position;
	}

    /*
     * When a player touches appropriate key, teleport the player into the next room.
     */
    private void OnCollisionEnter2D(Collision2D collision) {
        // Center the camera into the second room when player touches key.
        if (winKeyID == 1)
        {
            GameObject.Find("Main Camera").GetComponent<Camera>().transform.position = GameObject.Find("Room2 Center").transform.position;
            SpawnPoints secondSpawn = GameObject.Find("Spawn2").GetComponent<SpawnPoints>();

            // Teleport the player that touched special key into the next room.
            if (collision.gameObject.tag == "Player1")
            {
                collision.gameObject.transform.position = secondSpawn.spawnPoints[0].transform.position;
            }
            if (collision.gameObject.tag == "Player2")
            {
                collision.gameObject.transform.position = secondSpawn.spawnPoints[1].transform.position;
            }
            if (collision.gameObject.tag == "Player3")
            {
                collision.gameObject.transform.position = secondSpawn.spawnPoints[2].transform.position;
            }
            if (collision.gameObject.tag == "Player4")
            {
                collision.gameObject.transform.position = secondSpawn.spawnPoints[3].transform.position;
            }
        }

        if (winKeyID == 2)
        {
            GameObject.Find("Main Camera").GetComponent<Camera>().transform.position = GameObject.Find("Room3 Center").transform.position;
            SpawnPoints thirdSpawn = GameObject.Find("Spawn3").GetComponent<SpawnPoints>();

            if (collision.gameObject.tag == "Player1")
            {
                collision.gameObject.transform.position = thirdSpawn.spawnPoints[0].transform.position;
            }
            if (collision.gameObject.tag == "Player2")
            {
                collision.gameObject.transform.position = thirdSpawn.spawnPoints[1].transform.position;
            }
            if (collision.gameObject.tag == "Player3")
            {
                collision.gameObject.transform.position = thirdSpawn.spawnPoints[2].transform.position;
            }
            if (collision.gameObject.tag == "Player4")
            {
                collision.gameObject.transform.position = thirdSpawn.spawnPoints[3].transform.position;
            }
        }
    }
}
