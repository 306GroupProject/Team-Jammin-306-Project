using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RedBarrel : NetworkBehaviour {

    public GameObject explosion;

    // destroys gate for room2 after destroyed by fireball
    public GameObject winGate2;

    private void OnCollisionEnter2D(Collision2D other)
    {
        // Red Barrel can only be destroyed by Fireball
        if (other.gameObject.tag == "Fireball")
        {
            GameObject potato = Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(winGate2);
            Destroy(this.gameObject);
            Destroy(potato, potato.GetComponent<ParticleSystem>().main.duration / 2.0f);
        }
    }

}
