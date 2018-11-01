using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CameraMove : NetworkBehaviour {
    public Camera cam_main; // current levels camera
    public Camera cam_next; // camera for the next room

    private void Start()
    {
        cam_main.enabled = true; // first room is only visble
        cam_next.enabled = false; // second is not visible
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player1" || other.gameObject.tag == "Player2" || other.gameObject.tag == "Player3" || other.gameObject.tag == "Player4")
        {
            cam_main.enabled = false; // first level is complete camera off
            cam_next.enabled = true; // second level 
        }
     
    }

}
