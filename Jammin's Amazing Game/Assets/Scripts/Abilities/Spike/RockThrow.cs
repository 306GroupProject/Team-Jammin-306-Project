using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockThrow : Abilities {

    public void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            Cast();
        }
    }

    public override void Cast() {
        CmdCast();
    }

    public override void CmdCast() {
        RpcCast();
    }

    public override void RpcCast() {
        Debug.Log("Casted Rock Throw!");
    }
}
