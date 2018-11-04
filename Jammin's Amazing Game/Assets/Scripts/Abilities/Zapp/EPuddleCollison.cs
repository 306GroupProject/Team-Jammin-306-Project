using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/*
 * Handles Electric Puddle Collision
 */
public class EPuddleCollison : NetworkBehaviour
{
    [SerializeField, SyncVar]
    private int damage = 1;

    public int dotRate = 1;
    float startTime;

    public void Awake() {
        startTime = Time.time;
    }

    public void OnTriggerStay2D(Collider2D collision) {
        if ((collision.gameObject.GetComponent("ai") as ai) != null) {
            if (Time.time > startTime) {
                collision.gameObject.SendMessage("Damage", damage);
                startTime = Time.time + dotRate;
            }
        }
    }


}
