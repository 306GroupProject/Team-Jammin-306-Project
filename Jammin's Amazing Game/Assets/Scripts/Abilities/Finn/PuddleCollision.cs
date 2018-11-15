using UnityEngine;
using UnityEngine.Networking;

/*
 * Handles Puddle Collision
 */
public class PuddleCollision : NetworkBehaviour
{

    [SerializeField, SyncVar]
    public float slowRate = 50.0f;
    PlayerManager Script;
    bool notelectric = true;

    public GameObject electricPuddle;

    /*
    * Slows a player when they enter the puddle, and electrifies the puddle if hit by a bolt
    */
    public void OnTriggerStay2D(Collider2D collision)
    {
        
        if(collision.gameObject.tag == "Player1" || collision.gameObject.tag == "Player2" || collision.gameObject.tag == "Player3" || collision.gameObject.tag == "Player4")
        {
            Script = collision.gameObject.GetComponent<PlayerManager>();
            Script.changeSpeed(slowRate);
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
        if (collision.gameObject.tag == "Player1" || collision.gameObject.tag == "Player2" || collision.gameObject.tag == "Player3" || collision.gameObject.tag == "Player4") {
            Script = collision.gameObject.GetComponent<PlayerManager>();
            Script.changeSpeed(100.0f);
        }
    }
}
