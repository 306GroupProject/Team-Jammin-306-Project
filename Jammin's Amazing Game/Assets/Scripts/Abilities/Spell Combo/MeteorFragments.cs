using UnityEngine.Networking;
using UnityEngine;

public class MeteorFragments : MonoBehaviour {

    public float airTime = 3.0f;
    public int damage = 2;

	// Update is called once per frame
	void Update () {
        Destroy(this.gameObject, airTime);
	}

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Enemy") {
            collision.gameObject.SendMessage("Damage", damage);
        }
        Destroy(this.gameObject);
    }
}
