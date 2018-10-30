using UnityEngine;
using UnityEngine.Networking;

/**
 * Controls the pebbles spawned after rockthrow is destroyed
 */ 
public class Pebble : NetworkBehaviour {

    public float airTime = 3.0f;
    public int damage = 1;
    public GameObject fireFragments;

    // Cross Trajectory Positions
    Vector2[] crossPositions = { Vector2.up, Vector2.left, Vector2.down, Vector2.right};
    // Rotate fire frags at the right direction
    float[] rotations = { 0, 90.0f, 190.0f, -90.0f };

    // How fast fire fragments should travel
    public float fireFragVelocity = 2000.0f;

    public void Update() {
        Destroy(this.gameObject, airTime);
    }

    // If the pebble collides with a fragment, instantiate more fire fragments. Also send damage if hits a Damagable object,
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Fireball" || collision.gameObject.tag == "FireFrags") {
            for (int i = 0; i < rotations.Length; i++) {
                GameObject frag = Instantiate(fireFragments, transform.position, Quaternion.Euler(new Vector3(0,0,rotations[i])));
                frag.GetComponent<Rigidbody2D>().AddForce(crossPositions[i] * fireFragVelocity);
            }
        }

        collision.gameObject.SendMessage("Damage", damage, SendMessageOptions.DontRequireReceiver);
        Destroy(this.gameObject);
    }

    // Controls pebble velocity.
    public void CmdCast(Vector2 rockTransform, Vector2 moveTo, float velocity) {
        Rigidbody2D rockRb = this.GetComponent<Rigidbody2D>();
        rockRb.AddForce(moveTo * velocity);
        rockRb.AddTorque(100);
    }

}
