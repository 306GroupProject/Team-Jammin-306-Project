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

    // Diagonal trajectories
    static Vector2 diagLeft = new Vector2(-Mathf.Sqrt(2) / 2, Mathf.Sqrt(2) / 2);
    static Vector2 diagRight = new Vector2(Mathf.Sqrt(2) / 2, -Mathf.Sqrt(2) / 2);
    static Vector2 diagDLeft = new Vector2(-Mathf.Sqrt(2) / 2, -Mathf.Sqrt(2) / 2);
    static Vector2 diagDRight = new Vector2(Mathf.Sqrt(2) / 2, Mathf.Sqrt(2) / 2);

    Vector2[] crossPositions = { Vector2.up, diagLeft, Vector2.left, diagDLeft, Vector2.down, diagRight, Vector2.right, diagDRight };
    
    // Rotation where the meteor fragment should be facing.
    float[] rotations = { 0, 45.0f, 90.0f, 135.0f, 180.0f, 225.0f, 270.0f, 315.0f };

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

    /*
     * If the meteor collides with 
     */ 
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Enemy") {
            collision.gameObject.SendMessage("Damage", damage, SendMessageOptions.DontRequireReceiver);
            
        } else {
            for (int i = 0; i < crossPositions.Length; i++) {
                GameObject fragements = Instantiate(meteorFragments, transform.position, Quaternion.Euler(new Vector3(0, 0, rotations[i])));
                fragements.GetComponent<Rigidbody2D>().AddForce(crossPositions[i] * fragForce);

            }
        }
        Destroy(this.gameObject);
    }

}
