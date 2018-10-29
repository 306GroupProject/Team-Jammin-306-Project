using UnityEngine.Networking;
using UnityEngine;

public class MeteorFragments : MonoBehaviour {

    public float airTime = 3.0f;
    public int damage = 2;
    Vector2[] crossPositions = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };

    // Update is called once per frame
    void Update () {
        Destroy(this.gameObject, airTime);
	}

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Pebbles") {
            foreach (Vector2 pos in crossPositions) {
                GameObject frag = Instantiate(this.gameObject, transform.position, Quaternion.identity);
                frag.GetComponent<Rigidbody2D>().AddForce(collision.gameObject.GetComponent<Pebble>().fireFragVelocity * pos);
            }

        }
        if (collision.gameObject.tag == "Enemy") {
            collision.gameObject.SendMessage("Damage", damage);
        }
        Destroy(this.gameObject);
    }
}
