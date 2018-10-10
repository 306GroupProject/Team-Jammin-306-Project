using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class TestMovement : NetworkBehaviour {

    private Rigidbody2D rb;
    public float speed = 50.0f;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();

		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (!this.GetComponent<NetworkIdentity>().isLocalPlayer) {
            return;
        }

        Vector2 vertical = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        rb.AddForce(vertical * speed);
    }
}
