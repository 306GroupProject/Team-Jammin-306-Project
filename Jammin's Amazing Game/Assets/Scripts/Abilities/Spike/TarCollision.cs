using UnityEngine;
using UnityEngine.Networking;

public class TarCollision : NetworkBehaviour {

    [SerializeField, SyncVar]
    public float slowRate = 100.0f;
    PlayerManager Script;

    int lifeTime = 7;
    public GameObject tarFire;

    /*
    * Slows a player when they enter the puddle, and electrifies the puddle if hit by a bolt
    */
    void Start() {
        InvokeRepeating("restoreSpeed", 0.0f, 1.0f);

        Destroy(this.gameObject, tarFire.GetComponent<ParticleSystem>().main.duration);
    }


    /*
     * Triggers when something is inside the collider trigger
     */ 
    public void OnTriggerStay2D(Collider2D collision) {
        // Check if a player is in trigger.
        if (collision.gameObject.tag == "Player1" || collision.gameObject.tag == "Player2" || collision.gameObject.tag == "Player3" || collision.gameObject.tag == "Player4") {
            Script = collision.gameObject.GetComponent<PlayerManager>();
            Script.changeSpeed(slowRate);
 
        }
        // If a fireball collides with tar, set it on fire!
        if (collision.gameObject.tag == "Fireball") {
            Destroy(collision.gameObject);
            GameObject fire = Instantiate(tarFire, transform.position, Quaternion.identity);
            Destroy(fire.gameObject, fire.GetComponent<ParticleSystem>().main.duration);
            Destroy(this.gameObject, fire.GetComponent<ParticleSystem>().main.duration);
        }
    }

    /*
    * Restores the players original speed when they leave the puddle
    */
    public void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player1" || collision.gameObject.tag == "Player2" || collision.gameObject.tag == "Player3" || collision.gameObject.tag == "Player4") {
            Script = collision.gameObject.GetComponent<PlayerManager>();
            Script.changeSpeed(100.0f);
        }
    }

    /*
     * Restore player speed
     */ 
    private void restoreSpeed() {
        lifeTime = lifeTime - 1;
        if (lifeTime == 0 && Script != null) {
            Script.changeSpeed(100);
            Destroy(this.gameObject, tarFire.GetComponent<ParticleSystem>().main.duration);
        }
    }
}
