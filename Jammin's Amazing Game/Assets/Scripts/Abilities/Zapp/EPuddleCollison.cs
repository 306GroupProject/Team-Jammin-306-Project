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
    private float damage = 0.01f;

    public void OnCollisionStay2D(Collision2D collision)
    {
        if ((collision.gameObject.GetComponent("ai") as ai) != null)
        {
            collision.gameObject.SendMessage("Damage", damage);
        }
    }
}
