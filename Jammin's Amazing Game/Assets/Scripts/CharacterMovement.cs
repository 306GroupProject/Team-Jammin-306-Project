using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking; 



public class CharacterMovement : MonoBehaviour {

    [SerializeField] private float speed = 20.0f; // use to calculate how much force to be added. If player slips a lot, modify linear drag in RB toolbar
    private Rigidbody2D rb; // Get the RigidBody Component of our player to access mass and force
    private Animator anim;


	void Start () {
        rb = GetComponent<Rigidbody2D>();   // Obtain rigid body component to whom this script is attached to
        anim = GetComponent<Animator>();    // Obtain animator to access varibles set to player animator
        anim.SetBool("IsMoving", false);   
	}
	
	
	void FixedUpdate () {

		if (!this.GetComponent<NetworkIdentity>().isLocalPlayer) { // well allow me to control my player but not every other character in the scene.

			return; 


		}

        // The next two lines set the variable attached to the player animator to what the current horizontal and vertical axis values are.
        anim.SetFloat("Vertical Movement", Input.GetAxis("Vertical"));      // Set vertical animator float value to current vertical axis
        anim.SetFloat("Horizontal Movement", Input.GetAxis("Horizontal"));  // Set horizontal animator float value to current horizontal axis


        // Create a vector2 object, with x being the current horizontal axis and y as the current Vertical Axis
        Vector2 movement = new Vector2(anim.GetFloat("Horizontal Movement"), anim.GetFloat("Vertical Movement"));


        if ((anim.GetFloat("Horizontal Movement") == 0) && (anim.GetFloat("Vertical Movement") == 0)) {
            anim.SetBool("IsMoving", false);
        } 
        else {
            anim.SetBool("IsMoving", true);
        }


        rb.AddForce(movement * speed);
        
	}
}
