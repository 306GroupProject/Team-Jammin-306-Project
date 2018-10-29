using UnityEngine;
using UnityEngine.Networking;

/**
 * Controls the pebbles spawned after rockthrow is destroyed
 */ 
public class Pebble : NetworkBehaviour {

    public float airTime = 3.0f;
    public int damage = 1;
    public GameObject fireFragments;
    Vector2[] crossPositions = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
    public float fireFragVelocity = 2000.0f;

    public void Update() {
        Destroy(this.gameObject, airTime);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Fireball" || collision.gameObject.tag == "FireFrags") {
            foreach (Vector2 pos in crossPositions) {
                GameObject frag = Instantiate(fireFragments, transform.position, Quaternion.identity);
                frag.GetComponent<Rigidbody2D>().AddForce(pos * fireFragVelocity);
            }
        }

        collision.gameObject.SendMessage("Damage", damage, SendMessageOptions.DontRequireReceiver);
        Destroy(this.gameObject);
    }


    public void CmdCast(Vector2 rockTransform) {
        Vector2 randomVector = new Vector2(Random.Range(-100, 100), Random.Range(-100, 100));
        Vector2 direction = (rockTransform - randomVector).normalized;
        Rigidbody2D rockRb = this.GetComponent<Rigidbody2D>();
        rockRb.AddForce(direction * Random.Range(100, 1000));
        rockRb.AddTorque(100);
    }

}
