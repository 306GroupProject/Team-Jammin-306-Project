using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking; 



public class CharacterMovement : NetworkBehaviour {

    [SerializeField] public float speed = 20.0f; // use to calculate how much force to be added. If player slips a lot, modify linear drag in RB toolbar
    private Rigidbody2D rb; // Get the RigidBody Component of our player to access mass and force
    private Animator anim;

	public int plyerDmg = 4; 

    public GameObject attackPrefab; // Select the attack prefab you want drag and drop it into this variable in unity.
    public float attackRate = 0.5f; // cooldown for the basic attack
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

        // if the player's velocity is greater than 0.1, animate them walking
        if ((Mathf.Abs(anim.GetFloat("Horizontal Movement")) > 0.1) || (Mathf.Abs(anim.GetFloat("Vertical Movement")) > 0.1)) {
            anim.SetBool("IsMoving", true);

            // Flip
            if (Input.GetAxisRaw("Horizontal") > 0.0) {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            if (Input.GetAxisRaw("Horizontal") < 0.0) {
                transform.localScale = new Vector3(1, 1, 1);
            }
        } 
        else {
            anim.SetBool("IsMoving", false);
        }


        rb.AddForce(movement * speed);

        // initiates attack on left-click and starts cooldown
        if (Input.GetMouseButtonDown(0) && Time.time > canAttack)
        {
            CmdAttack();
            canAttack = Time.time + attackRate;
        }
        
	}

    // player attack command
    [Command]
    void CmdAttack()
    {
        Vector2 target = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        Vector2 myPos = new Vector2(transform.position.x, transform.position.y); // gets current cursor position
        Vector2 direction = target - myPos; // direction for the attack
        direction = direction.normalized; // normalize attack so it doesn't matter how close the cursor is to the player

        GameObject attack = (GameObject)Instantiate(attackPrefab, myPos, transform.rotation);
        attack.GetComponent<Rigidbody2D>().velocity = direction * 20.0f; // universal attack speed

		attack.gameObject.tag = "basicAttack"; 

        NetworkServer.Spawn(attack);
<<<<<<< HEAD
<<<<<<< HEAD

        Destroy(attack, 1.0f); // destroys the attack after set amount of time with no collision 
=======
=======





>>>>>>> parent of c9dcfba... Added Ability Asset Menu for player abilities! Also made small changes to some commands
        Destroy(attack, 1.0f);
>>>>>>> Player-Prefab-Management-Test
    }
}
