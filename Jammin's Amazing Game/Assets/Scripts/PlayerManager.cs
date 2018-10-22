using UnityEngine;
using UnityEngine.Networking;



public class PlayerManager : NetworkBehaviour {

    [SerializeField] public float speed = 20.0f; // use to calculate how much force to be added. If player slips a lot, modify linear drag in RB toolbar
    private Rigidbody2D rb; // Get the RigidBody Component of our player to access mass and force
    private Animator anim;

    public int plyerDmg = 4;

    //private Camera playerCam;
    public GameObject attackPrefab; // basic attack projectile
    public float attackRate = 0.5f; // Basic attack cooldown
    private float canAttack = 0.0f;
    private bool flipped = false;
    private SyncFlip flipMe;

    public float teleportCooldown = 3f;
    private float timeSinceTeleport = 0f;
    private Vector2 point; // the point where the mouse is clicked for a teleport
    private Transform playerTransform; // the player's position
    public GameObject teleportParticles;







    void Start() {
        rb = GetComponent<Rigidbody2D>();   // Obtain rigid body component to whom this script is attached to
        anim = GetComponent<Animator>();    // Obtain animator to access varibles set to player animator
        flipMe = GetComponent<SyncFlip>();
        
        anim.SetBool("IsMoving", false);
        //playerCam = GetComponentInChildren<Camera>();
        //playerCam.transform.position = new Vector3(rb.transform.position.x, rb.transform.position.y, -0.3f);
        //playerCam.fieldOfView = 177;

        playerTransform = GameObject.FindGameObjectWithTag("Player2").transform;

    }

    private void Update()
    {
        // if the player Right Clicks, teleport them to where they clicked.
        if(Input.GetMouseButtonDown(1))
        { 
            if(timeSinceTeleport <= Time.time)
            {
                Instantiate(teleportParticles, transform.position, Quaternion.identity);
                point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                // Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                playerTransform.transform.position = point;
                timeSinceTeleport = Time.time + teleportCooldown; // start the cooldown period
            } 
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

        // fire basic attack on mouse press, start and check cooldown
        if (Input.GetMouseButtonDown(0) && Time.time > canAttack) {
            CmdAttack();
            canAttack = Time.time + attackRate;
        }

    }
   
    // Create player attack, network Command
    [Command]
    void CmdAttack() {
        Vector2 target = Camera.main.ScreenToWorldPoint(Input.mousePosition); // make target mouse position
        Vector2 myPos = this.transform.position; // reference players current position
        Vector2 direction = target - myPos; // direction of the attack
        direction = direction.normalized; // normalize velcoity

        GameObject attack = (GameObject)Instantiate(attackPrefab, myPos, transform.rotation);
        attack.GetComponent<Rigidbody2D>().velocity = direction * 20.0f;
        // Instantiate attack prefab at players location with proper direction toward target

        attack.gameObject.tag = "basicAttack";

<<<<<<< HEAD
        NetworkServer.Spawn(attack); // spawns attack for all clients 
        Destroy(attack, 1.0f); // destroy projectile after set time
=======
        NetworkServer.Spawn(attack);

        Destroy(attack, 1.0f);
>>>>>>> 828eed099749ef4bab9c658c055d03d4711e9210

    }
}
