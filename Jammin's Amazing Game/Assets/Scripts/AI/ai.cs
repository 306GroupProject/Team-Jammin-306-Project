using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;




/**
 *  Interface for AI, if we want to add more AI to into the Game, then we can by using this 
 *  Interface.
 */ 
public abstract class ai : NetworkBehaviour {

	protected int playerSpotted = -1;  // this is the current target that our AI as spotted!
	protected float aiSavedSpeed; 	// the speed in which was given to the AI prefab!
	protected Animator anim;			// the animation to be played by the AI.
	protected int plyerDmg; 			// the damage that players deal to the AI.
	protected decisionTree rootOfTree;  // reference to Decision Tree, which points at root.



	public float aiMovementSpeed = 0;  // aiMovement will need to be set in unity. 
	public int aiDmg = 0; 			// aiDmg, will need to be set in unity
	public GameObject plyController; //THIS runs the entire script below. it accesses a SyncList. Check out the NetworkAssitant function, it explains it better.
}
