using UnityEngine;
using UnityEngine.Networking;

/*
 * Handles Fireball Collision. If Target is damagable, send message to run method damage.
 */
public class FireBallCollision : NetworkBehaviour {

    [SerializeField, SyncVar]
    private float damage = 4.0f;    // Syncvar fireball damage
    float startTime;
    public GameObject meteor;
    public GameObject explosion;
    public GameObject steam;

    public float airTime = 1.0f;

    private void Awake() {
        startTime = Time.time;
    }

    /*
     * Destroys fireball after a certain amount of time passed
     */ 
    void Update() {
        if (Time.time - startTime > airTime) {

            Vector2 save = transform.position;
            Destroy(this.gameObject);
            // Spawn explosion particle effect
            GameObject explode = Instantiate(explosion, save, Quaternion.identity);
            Destroy(explode, 2.0f);

        }
    }

    /*
     * Send Damage message if the enemy is struct by fireball. 
     */
    public void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.layer == 15) {
            collision.gameObject.SendMessage("Damage", damage);
        } else if (collision.gameObject.tag == "Water") {
            Destroy(this.gameObject);
            Destroy(collision.gameObject);
            GameObject steamer = Instantiate(steam, this.transform.position, Quaternion.identity);
            Destroy(steamer.gameObject, steamer.GetComponent<ParticleSystem>().main.duration / 2.0f);
        }
        Vector2 save = transform.position;
        Destroy(this.gameObject);

        // Spawn explosion particle effect
        GameObject explode = Instantiate(explosion, save, Quaternion.identity);
        Destroy(explode, 2.0f);
    }
}
