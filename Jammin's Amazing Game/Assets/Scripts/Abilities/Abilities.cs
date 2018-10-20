using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public abstract class Abilities : NetworkBehaviour {

    public string skillName;
    public string description;
    public float manaCost;
    public float cooldown;
    public GameObject projectile;

    public abstract void Cast();

    [Command]
    public abstract void CmdCast();

    [ClientRpc]
    public abstract void RpcCast();

}
