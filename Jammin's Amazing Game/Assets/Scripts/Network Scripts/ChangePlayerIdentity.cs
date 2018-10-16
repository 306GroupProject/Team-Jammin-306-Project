using UnityEngine;
using UnityEngine.Networking;

public class ChangePlayerIdentity : NetworkBehaviour {

    // Sync Variables so that changes to the network is sent across all connected clients
    [HideInInspector, SyncVar(hook = "ChangePlayerTag")] public string playerTag;
    [HideInInspector, SyncVar(hook = "ChangePlayerName")] public string playerName;

    /**
     * Changes the player's tag depending on the player they selected
     */
    void ChangePlayerTag(string tagName) {
        transform.tag = tagName;
    }

    /**
     * Changes the player's name.
    */
    void ChangePlayerName(string Name) {
        transform.name = Name;
    }
	
}
