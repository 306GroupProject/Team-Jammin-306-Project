using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class SyncFlip : NetworkBehaviour {

	[SyncVar(hook = "FlipMe")]
    public bool flipped = false;

    [Command]
    public void CmdFlip(bool facingRight) {
        RpcFlip(facingRight);
    }

    [ClientRpc]
    public void RpcFlip(bool facingRight) {
        if (facingRight) {
            transform.localScale = new Vector3(-1, 1, 1);
        } else {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public void FlipMe(bool facingRight) {
        if (facingRight) {
            transform.localScale = new Vector3(-1, 1, 1);
        } else {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
