using UnityEngine.Networking;
using UnityEngine;



public class SplashHeal : NetworkBehaviour {

    public GameObject particle;
    ParticleSystem fireWork;
    public int healRate = 1;

    // Each player have their own separate healing timer
    public float healRate1 = 1.0f;
    public float healRate2 = 1.0f;
    public float healRate3 = 1.0f;
    public float healRate4 = 1.0f;

    // Each player also get their own timers.
    float startTime1;
    float startTime2;
    float startTime3;
    float startTime4;

    

    private void Start() {
        startTime1 = Time.time;
        startTime2 = Time.time;
        startTime3 = Time.time;
        startTime4 = Time.time;
        fireWork = GetComponent<ParticleSystem>();
    }

    /*
     * If the particle collides with the player, heal!
     */ 
    public void OnParticleCollision(GameObject other) {
        if (other.gameObject.tag == "Player1" && Time.time > startTime1) {
            other.gameObject.SendMessage("Heal", healRate, SendMessageOptions.DontRequireReceiver);
            startTime1 = Time.time + healRate1;
        } if (other.gameObject.tag == "Player2" && Time.time > startTime2) {
            other.gameObject.SendMessage("Heal", healRate, SendMessageOptions.DontRequireReceiver);
            startTime2 = Time.time + healRate2;
        }
        if (other.gameObject.tag == "Player3" && Time.time > startTime3) {
            other.gameObject.SendMessage("Heal", healRate, SendMessageOptions.DontRequireReceiver);
            startTime3 = Time.time + healRate3;
        }
        if (other.gameObject.tag == "Player4" && Time.time > startTime4) {
            other.gameObject.SendMessage("Heal", healRate, SendMessageOptions.DontRequireReceiver);
            startTime4 = Time.time + healRate4;
        }
    }

    public void Update() {
        Destroy(this.gameObject, fireWork.main.duration);
    }
}
