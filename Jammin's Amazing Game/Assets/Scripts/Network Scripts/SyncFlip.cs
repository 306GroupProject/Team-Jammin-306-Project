using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

// Class that syncronizes player transform flips within the network.
public class SyncFlip : NetworkBehaviour {

	[SyncVar(hook = "FlipMe")]
    public bool flipped = false; // False if the player is facing right

    // Command that send flip information across all connected clients to the current host.
    [Command]
    public void CmdFlip(bool facingRight) {
        RpcFlip(facingRight);
    }

    // Client based command that controls player flip within the client.
    [ClientRpc]
    public void RpcFlip(bool facingRight) {
        if (facingRight) {
            transform.localScale = new Vector3(-1, 1, 1);
        } else {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    // Flips the character across all connected clients.
    public void FlipMe(bool facingRight) {
        if (facingRight) {
            transform.localScale = new Vector3(-1, 1, 1);
        } else {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
