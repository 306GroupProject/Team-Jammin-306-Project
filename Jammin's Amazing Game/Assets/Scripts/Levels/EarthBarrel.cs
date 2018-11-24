using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EarthBarrel : NetworkBehaviour {

    public GameObject winGate1;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Boulder")
        {
            Destroy(this.gameObject);
            Destroy(winGate1);
        }
    }

}
