using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
/*
 * Handles Lightning Collision
 */
public class LightningCollision : NetworkBehaviour
{

    [SerializeField, SyncVar]
    public float damage;

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.SendMessage("Damage", damage);
        }
    }
}
