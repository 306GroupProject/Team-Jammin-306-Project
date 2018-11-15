using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Barrel : NetworkBehaviour {

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Fireball")
        {
            
            Destroy(this.gameObject);
        }
    }

}
