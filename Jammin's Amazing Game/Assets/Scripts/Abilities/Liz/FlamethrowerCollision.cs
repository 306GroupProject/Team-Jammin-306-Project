using UnityEngine.Networking;
using UnityEngine;

public class FlamethrowerCollision : MonoBehaviour {

    ParticleSystem flames;  // We need to access the particle effect attached to this to access collisions with it.
    public int damageOverTime = 2;
    public float damageRate = 1.0f; 
    float startTime;

    private void Start() {
        startTime = Time.time;
        flames = GetComponent<ParticleSystem>();
    }

    /*
     * Check if the instantiated particle representing the flame thrower collided with the  object
     */ 
    private void OnParticleCollision(GameObject other) {
        if (other.tag == "Enemy") {
            if (Time.time > startTime) {
                other.gameObject.SendMessage("Damage", damageOverTime);
                startTime = Time.time + damageRate;
                print(damageOverTime);
            }
        }
    }
}
