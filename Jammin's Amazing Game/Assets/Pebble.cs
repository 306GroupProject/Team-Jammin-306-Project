using UnityEngine;
using UnityEngine.Networking;

/**
 * Controls the pebbles spawned after rockthrow is destroyed
 */ 
public class Pebble : NetworkBehaviour {

    public float airTime = 3.0f;
    public int damage = 1;
    float startTime;

    public void Awake() {
        startTime = Time.time;
    }

    public void Update() {
        if (Time.time - startTime > airTime) {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
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
