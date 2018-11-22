using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Barrel : NetworkBehaviour {

    public GameObject explosion;

    private void OnCollisionEnter2D(Collision2D other)
    {
        // Red Barrel can only be destroyed by Fireball for now
        if (other.gameObject.tag == "Fireball") // looking to create a totem so Spike could also destroy it
        {
            GameObject potato = Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            Destroy(potato, potato.GetComponent<ParticleSystem>().main.duration / 2.0f);
        }
    }

}
