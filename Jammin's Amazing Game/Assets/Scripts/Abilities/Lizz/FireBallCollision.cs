using UnityEngine;
using UnityEngine.Networking;

/*
 * Handles Fireball Collision. If Target is damagable, send message to run method damage.
 */
public class FireBallCollision : NetworkBehaviour {

    [SerializeField, SyncVar]
    private float damage = 4.0f;    // Syncvar fireball damage

    public GameObject meteor;

    public GameObject explosion;
    /*
     * Send Damage message if the enemy is struct by fireball. 
     */
    public void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Enemy") {
            collision.gameObject.SendMessage("Damage", damage);
        }
        Vector2 save = transform.position;
        Destroy(this.gameObject);

        // Spawn explosion particle effect
        GameObject explode = Instantiate(explosion, save, Quaternion.identity);
        Destroy(explode, 2.0f);
    }
}
