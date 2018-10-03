using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

// For this script to work properly it needs a 2 GameObjects: the Camera Manager(not an official thing I just made it) and a Camera.

public class Starting_Camera_Script : NetworkBehaviour {
    

    // The camera that is enabled before the player connects to the game (normally some sort of menu screen)
    public Camera starting_cam;

    /* public void OnLobbyStartHost()
    {
        Debug.Log("server started/ host joined");
        starting_cam.enabled = false;
    }

    public void OnLobbyClientEnter() {
        Debug.Log("player joined");
        starting_cam.enabled = false;
    } */

    public override void OnStartClient() // The overrride keywork is used to make the script of the starting camera work. This function is called when a client is started
    {
        Debug.Log("player joined");
        starting_cam.enabled = false;
    }
}
