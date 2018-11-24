using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EarthBarrel : NetworkBehaviour {

    // Room 3 gates
    public GameObject winGate1;

    // Destroy one of the gates after barrel collides with Boulder
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Boulder")
        {
            Destroy(this.gameObject);
            Destroy(winGate1);
        }
    }

}
