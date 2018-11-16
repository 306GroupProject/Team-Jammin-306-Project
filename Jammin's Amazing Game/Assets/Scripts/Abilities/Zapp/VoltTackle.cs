using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/**
 * Volt Tackle Ability for Zapp. Inherits Abilities!
 */
public class VoltTackle : Abilities {

    [SerializeField, SyncVar]
    private float canAttack;
    public GameObject spark;
    GameObject particle;
    bool trail = false;
    private float duration = 0.3f;
    private float elapsed;
    private float damage = 10.0f;

    public void Update()
    {
        // Only allow local player to cast, so that other players doesn't cast this ability as well
        if (isLocalPlayer)
        {
            if (Input.GetKeyDown(KeyCode.Alpha3) && Time.time > canAttack)
            {
                CmdCast(transform.position, (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition));
                canAttack = Time.time + cooldown;
            }
        }

        // Creates a trail of particle for a short duration during the dash
        if (trail)
        {
            particle = Instantiate(spark, transform.position, Quaternion.identity);
            particle.tag = "Bolt";
            Destroy(particle, 0.5f);
            if(elapsed < Time.time)
            {
                trail = false;
            }
        }
    }

    [Command]
    void CmdCast(Vector2 playerTransform, Vector2 mouseTransform)
    {
        RpcCast(playerTransform, mouseTransform);
    }

    [ClientRpc]
    void RpcCast(Vector2 playerTransform, Vector2 mouseTransform)
    {
        Vector2 direction = (mouseTransform - playerTransform).normalized;      
        trail = true;
        elapsed = Time.time + duration;
        this.GetComponent<Rigidbody2D>().AddForce(direction * 10000.0f);
    }

    /*
     * Only checks for collision while the player is dashing more or less
     */
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && trail)
        {
            collision.gameObject.SendMessage("Damage", damage);
        }
    }
}
