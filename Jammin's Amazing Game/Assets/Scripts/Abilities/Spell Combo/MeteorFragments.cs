using UnityEngine.Networking;
using UnityEngine;

public class MeteorFragments : MonoBehaviour {

    public float airTime = 3.0f;
    public int damage = 2;
    Vector2[] crossPositions = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
    float[] rotations = { 0, 180.0f, 90.0f, -90.0f };
    public GameObject explosion;
    float startTime;

    private void Awake() {
        startTime = Time.time;
    }

    // Destroy this object once a certain amout of time passes.
    void Update () {
        if (Time.time - startTime > airTime) {
            Vector2 save = transform.position;
            Destroy(this.gameObject);
            GameObject explode = Instantiate(explosion, save, Quaternion.identity);
            explode.transform.localScale /= 6;
            Destroy(explode, 2.0f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        // If our meteor fragment cocllids with a pebble, 4 fragments in a cross pattern
        if (collision.gameObject.tag == "Pebbles") {
            for (int i = 0; i < rotations.Length; i++ ) {
                GameObject frag = Instantiate(this.gameObject, transform.position, Quaternion.Euler(new Vector3(0,0,rotations[i])));
                frag.GetComponent<Rigidbody2D>().AddForce(collision.gameObject.GetComponent<Pebble>().fireFragVelocity * crossPositions[i]);
            }

        }
        if (collision.gameObject.tag == "Enemy") {
            collision.gameObject.SendMessage("Damage", damage);
        }
        Vector2 save = transform.position;
        Destroy(this.gameObject);
        GameObject explode = Instantiate(explosion, save, Quaternion.identity);
        explode.transform.localScale /= 6;
        Destroy(explode, 2.0f);
    }
}
