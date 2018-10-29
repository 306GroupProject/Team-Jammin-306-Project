using UnityEngine;
using UnityEngine.Networking;

/*
 * Meteor Script that handles meteor collisions.
 */ 
public class Meteor : NetworkBehaviour {

    public float velocity = 500.0f;     
    [SerializeField] float airTime = 5.0f;
    int damageMultiplier = 2;       // Multiply damage of meteor by 2 * fireball damage.
    public int damage;
    [SerializeField] GameObject meteorFragments;
    public int spreadRate = 4;
    public float fragForce = 300.0f;

    /*
     * Sets up player damage by getting the Synced damage value of meteor, by the fireball damage
     */ 
    public void Awake() {
        damage *= damageMultiplier;
    }

    // Keep track of meteor's airtime
    void Update () {
        Destroy(this.gameObject, airTime);
    }


    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Enemy") {
            collision.gameObject.SendMessage("Damage", damage, SendMessageOptions.DontRequireReceiver);
        }
        for (int i = 0; i < spreadRate; i++) {
            GameObject fragements = Instantiate(meteorFragments, transform.position, Quaternion.identity);
            fragements.GetComponent<Rigidbody2D>().AddForce(Random.insideUnitCircle * fragForce);
            fragements.GetComponent<Rigidbody2D>().AddTorque(1000f);

        }
        Destroy(this.gameObject);
    }

}
