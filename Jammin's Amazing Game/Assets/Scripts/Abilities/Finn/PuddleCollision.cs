using UnityEngine;
using UnityEngine.Networking;

/*
 * Handles Puddle Collision
 */
public class PuddleCollision : NetworkBehaviour
{

    [SerializeField, SyncVar]
    private float damage = 0.0f;
    PlayerManager Script;
    bool notelectric = true;

    public GameObject electricPuddle;

    /*
    * Slows a player when they enter the puddle, and electrifies the puddle if hit by a bolt
    */
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if((collision.gameObject.GetComponent("PlayerManager") as PlayerManager) != null)
        {
            Script = collision.gameObject.GetComponent<PlayerManager>();
            Script.changeSpeed(50.0f);
        }
        if (collision.gameObject.tag == "Bolt" && notelectric)
        {
            print("asdasdasdasd");
            notelectric = false;
            GameObject EPuddle = Instantiate(electricPuddle, transform.position, Quaternion.identity);
            Destroy(EPuddle, 5.0f);
            Destroy(this.gameObject);
            
        }
    }

    /*
    * Restores the players original speed when they leave the puddle
    */
    public void OnTriggerExit2D(Collider2D collision)
    {
        Script = collision.gameObject.GetComponent<PlayerManager>();
        Script.changeSpeed(100.0f);
    }
}
