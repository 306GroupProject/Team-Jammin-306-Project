using UnityEngine;
using UnityEngine.Networking;

/**
 * Splatter is a class that controls rockThrow collisions and interactions 
 * after being casted 
 */
public class Splatter : NetworkBehaviour {
    public int damage = 4;
    public float airtime;
    public int splashRate;
    public GameObject pebble;

    float startTime;
    private void Awake() {
        startTime = Time.time;
    }

    public void Update() {

        // Destroy the casted rock throw after X amount of seconds.
        if (Time.time - startTime > airtime) {
            // Loops that spawns out the projectiles after being destroyed mid-air.
            for (int i = 0; i < splashRate; i++) {
            GameObject pebbles = Instantiate(pebble, transform.position, Quaternion.identity);
            pebbles.GetComponent<Pebble>().CmdCast(transform.position);
        }
            Destroy(this.gameObject);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision) {
        // Sends damage message to the object rock collided with
        collision.gameObject.SendMessage("Damage", damage, SendMessageOptions.DontRequireReceiver);
        // Splash pebbles.
        for (int i = 0; i < splashRate; i++) {
            GameObject pebbles = Instantiate(pebble, transform.position, Quaternion.identity);
            pebbles.GetComponent<Pebble>().CmdCast(transform.position);
        }
        
        NetworkServer.Destroy(this.gameObject);
    }




}
