using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class TestMovement : NetworkBehaviour {

    private Rigidbody2D rb;
    private Camera playerCam;
    public float speed = 50.0f;
    private SyncFlip flipMe;
    private bool flipped;
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        playerCam = GetComponentInChildren<Camera>();
        playerCam.transform.position = new Vector3(rb.transform.position.x, rb.transform.position.y, -0.3f);
        playerCam.fieldOfView = 177;
        flipMe = GetComponent<SyncFlip>();


    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (!this.GetComponent<NetworkIdentity>().isLocalPlayer) {
            return;
        }
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
        Vector2 vertical = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        rb.AddForce(vertical * speed);
    }
}
