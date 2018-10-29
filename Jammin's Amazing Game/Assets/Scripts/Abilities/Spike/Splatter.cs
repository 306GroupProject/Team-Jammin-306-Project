using UnityEngine;
using UnityEngine.Networking;

/**
 * Splatter is a class that controls rockThrow collisions and interactions 
 * after being casted 
 */
public class Splatter : NetworkBehaviour {

    [SyncVar] public int damage = 4;
    public float airtime;
    public int splashRate;
    public GameObject pebble;
    [HideInInspector] public Vector2 trans;
    [SerializeField] GameObject meteor;
    float startTime;
    Vector2[] positions = { Vector2.left, Vector2.right, Vector2.up, Vector2.down};
    float[] rotations = { 90.0f, -90.0f, 0.0f, 180.0f };
    [SyncVar] int random;

    /*
     * Awake starts our timer for destroying projectile mid-air
     */
    private void Awake() {
        startTime = Time.time;
    }

    /*
     * Update keeps track of our boulder's airtime. If it is destroyed mid air, spawn
     * mini-projectiles that deals small damage to enemies.
     */ 
    public void Update() {
        random = Random.Range(0, 2);
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

    /*
     * Collision detector
     */ 
    public void OnCollisionEnter2D(Collision2D collision) {
        // Don't spawn in pebbles if collides with a fireball. Will summon meteors!
        if (collision.gameObject.tag == "Fireball") {

            Vector2 spawnMeteorPoint = collision.gameObject.transform.position;  

            // Spawns 4 meteors in cross-shaped pattern with             
            for (int i = 0; i < 4; i++) {
                Vector2 direction = positions[i];
                Quaternion rotation = Quaternion.Euler(new Vector3(0,0,rotations[i]));
                GameObject fire = Instantiate(meteor, spawnMeteorPoint, rotation);
                fire.GetComponent<Rigidbody2D>().AddForce(direction.normalized * fire.GetComponent<Meteor>().velocity);
                meteor.GetComponent<Meteor>().damage = this.damage;
            }

        } else {
            // Sends damage message to the object rock collided with
            collision.gameObject.SendMessage("Damage", damage, SendMessageOptions.DontRequireReceiver);
            // Splash pebbles.
            for (int i = 0; i < splashRate; i++) {
                GameObject pebbles = Instantiate(pebble, transform.position, Quaternion.identity);
                pebbles.GetComponent<Pebble>().CmdCast(transform.position);
            }
        }
        NetworkServer.Destroy(this.gameObject);
    }

    


}
