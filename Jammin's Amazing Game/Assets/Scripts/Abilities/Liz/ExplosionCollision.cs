using UnityEngine.Networking;
using UnityEngine;

public class ExplosionCollision : NetworkBehaviour {

    ParticleSystem explosion;
    public GameObject meteor;
    float canDamage;
    public float damageRate = 1.0f;
    public int damageTick = 3;
    float startTime;
    Vector2[] positions = { Vector2.left, Vector2.right, Vector2.up, Vector2.down };
    float[] rotations = { 90.0f, -90.0f, 0.0f, 180.0f };

    // Trajectory directions
    static Vector2 diagLeft = new Vector2(-Mathf.Sqrt(2) / 2, Mathf.Sqrt(2) / 2);
    static Vector2 diagRight = new Vector2(Mathf.Sqrt(2) / 2, -Mathf.Sqrt(2) / 2);
    static Vector2 diagDLeft = new Vector2(-Mathf.Sqrt(2) / 2, -Mathf.Sqrt(2) / 2);
    static Vector2 diagDRight = new Vector2(Mathf.Sqrt(2) / 2, Mathf.Sqrt(2) / 2);

    Vector2[] crossPositions = { Vector2.up, diagLeft, Vector2.left, diagDLeft, Vector2.down, diagRight, Vector2.right, diagDRight };


    // Use this for initialization
    void Start () {
        explosion = GetComponent<ParticleSystem>();
	}

    /*
     * Checks particle collision to handle enemy damage.
     */ 
    private void OnParticleCollision(GameObject collision) {
        if (collision.gameObject.tag == "Enemy" && Time.time > canDamage) {
            canDamage = Time.time + damageRate;
            collision.SendMessage("Damage", damageTick, SendMessageOptions.DontRequireReceiver);
        }

        // If explosion hits a boulder, combine!
        if (collision.gameObject.tag == "Boulder") {

            Vector2 spawnMeteorPoint = collision.gameObject.transform.position;

            // Spawns 4 meteors in cross-shaped pattern with             
            for (int i = 0; i < 4; i++) {
                Vector2 direction = positions[i];
                Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, rotations[i]));
                GameObject fire = Instantiate(meteor, spawnMeteorPoint, rotation);
                fire.GetComponent<Rigidbody2D>().AddForce(direction.normalized * fire.GetComponent<Meteor>().velocity);
                meteor.GetComponent<Meteor>().damage = this.damageTick;
            }
            Destroy(collision.gameObject);
        }
    }
}
