using UnityEngine.Networking;
using UnityEngine;

public class ExplosionCollision : NetworkBehaviour {

    ParticleSystem explosion;
    float canDamage;
    public float damageRate = 1.0f;
    public int damageTick = 3;

	// Use this for initialization
	void Start () {
        explosion = GetComponent<ParticleSystem>();
	}

    /*
     * Checks particle collision to handle enemy damage.
     */ 
    private void OnParticleCollision(GameObject other) {
        if (other.gameObject.tag == "Enemy" && Time.time > canDamage) {
            canDamage = Time.time + damageRate;
            other.SendMessage("Damage", damageTick, SendMessageOptions.DontRequireReceiver);
        }
    }
}
