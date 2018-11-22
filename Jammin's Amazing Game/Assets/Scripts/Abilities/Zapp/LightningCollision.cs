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
    private float damage = 1.0f;

    public void OnCollisionStay2D(Collision2D collision)
    {      
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.SendMessage("Damage", damage);
        }
    }
}
