using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/**
 *  Interface for Player Abilities, which inherits Network Behaviour
 *  This will allow us to easily create abilites that shares common variables
 *  and methods
 */ 
public abstract class Abilities : NetworkBehaviour {

    public string skillName;    
    public string description; // Skill Description
    [SyncVar] public float velocity;     // How fast to throw some projectile at enemy
    [SyncVar] public float manaCost;      
    [SyncVar] public float cooldown;
    public GameObject projectile;

    /**
     * CmdCast allows us to send a method call from the client to the server. This
     * allows us to run the function accross all connected clients in the current game
     */
    [Command]
    public abstract void CmdCast(Vector2 playerTransform, Vector2 mouseTransform);

    /**
     * Client Rpc allows us to invoke a method over severs after a command is called.
     * This ensures that our method is invoked accross all connected clients and not 
     * just the local player
     */ 
    [ClientRpc]
    public abstract void RpcCast(Vector2 playerTransform, Vector2 mouseTransform);

}
