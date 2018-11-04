using UnityEngine;
using UnityEngine.Networking;



public class PlayerManager : NetworkBehaviour {

    [SyncVar (hook = "changeSpeed")] public float speed = 20.0f; // use to calculate how much force to be added. If player slips a lot, modify linear drag in RB toolbar
    private Rigidbody2D rb; // Get the RigidBody Component of our player to access mass and force
    private Animator anim;

    public int plyerDmg = 4;


    private bool flipped = false;
    private SyncFlip flipMe;

    public void changeSpeed(float speed){

		this.speed = speed; 

	}

    void Start() {
        rb = GetComponent<Rigidbody2D>();   // Obtain rigid body component to whom this script is attached to
        anim = GetComponent<Animator>();    // Obtain animator to access varibles set to player animator
        flipMe = GetComponent<SyncFlip>();

        SpawnPoints spawnPoint = GameObject.Find("Spawn1").GetComponent<SpawnPoints>();

        anim.SetBool("IsMoving", false);
        if (this.tag == "Player1") {
            this.transform.position = spawnPoint.spawnPoints[0].transform.position;
        }
        else if (this.tag == "Player2") {
            this.transform.position = spawnPoint.spawnPoints[1].transform.position;
        }
        else if (this.tag == "Player3") {
            this.transform.position = spawnPoint.spawnPoints[2].transform.position;
        }
        else if (this.tag == "Player4") {
            this.transform.position = spawnPoint.spawnPoints[3].transform.position;
        }
    }

    void FixedUpdate() {


        if (!this.GetComponent<NetworkIdentity>().isLocalPlayer) { // well allow me to control my player but not every other character in the scene.
            //playerCam.enabled = false;
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
                flipped = true;
                flipMe.CmdFlip(flipped);
                //transform.localScale = new Vector3(-1, 1, 1);
            }
            if (Input.GetAxisRaw("Horizontal") < 0.0) {
                flipped = false;
                flipMe.CmdFlip(flipped);
                //transform.localScale = new Vector3(1, 1, 1);
            }
        } else {
            anim.SetBool("IsMoving", false);
        }


        rb.AddForce(movement * speed);

    }
   
}
