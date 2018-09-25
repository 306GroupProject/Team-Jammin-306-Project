using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking; 



public class CharacterMovement : NetworkBehaviour {

    [SerializeField] private float speed = 20.0f; // use to calculate how much force to be added. If player slips a lot, modify linear drag in RB toolbar
    private Rigidbody2D rb; // Get the RigidBody Component of our player to access mass and force
    private Animator anim;


    public GameObject attackPrefab; //basic projecile
    public float attackRate = 0.50f; //attack cooldown
    private float canAttack = 0.0f; 



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

        
        //Fire basic attack
        if (Input.GetMouseButtonDown(0) && Time.time > canAttack)
        {
            CmdAttack();
            canAttack = Time.time + attackRate;
        }
        

	}
    
    // command prompt for user basic attack
    [Command]
    void CmdAttack()
    {
        Vector2 target = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y)); // target is the mouse location
        Vector2 myPos = new Vector2(transform.position.x, transform.position.y); // projectile based on players current position
        Vector2 direction = target - myPos; //direction of the projectile
        direction = direction.normalized;

        GameObject attack = (GameObject)Instantiate(attackPrefab, myPos, transform.rotation);
        attack.GetComponent<Rigidbody2D>().velocity = direction * 20.0f; //speed of projectile

        NetworkServer.Spawn(attack);

        Destroy(attack, 1.0f); //destroy after set time
    }
    
}
