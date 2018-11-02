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
    public string description;           // Skill Description
    [SyncVar] public float velocity;     // How fast to throw some projectile at enemy
    [SyncVar] public float manaCost;      
    [SyncVar] public float cooldown;
    public GameObject projectile;

}
