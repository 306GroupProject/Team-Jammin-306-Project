using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Fireball Ability for Lizz. Inherits Abilities!
 */
public class FireBall : Abilities
{

    private float canAttack;
    GameObject fireball;

    /**
     *  Fireball ability for Lizz, activated using number 1
     */
    public void Update()
    {
        // Only allow local player to cast, so that other players doesn't cast this ability as well
        if (isLocalPlayer)
        {
            if (Input.GetKeyDown(KeyCode.Alpha2) && Time.time > canAttack)
            {
                CmdCast(transform.position, (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition));
                canAttack = Time.time + cooldown;
            }
        }
    }


    public override void CmdCast(Vector2 playerTransform, Vector2 mouseTransform)
    {
        RpcCast(playerTransform, mouseTransform);
    }

    public override void RpcCast(Vector2 playerTransform, Vector2 mouseTransform)
    {
        // Spawns in a fireball, syncronized accross network!
        float angle = AngleMath(playerTransform, mouseTransform) + 90.0f;

        fireball = Instantiate(this.projectile, playerTransform, Quaternion.Euler(new Vector3(0f, 0f, angle)));
        Vector2 direction = (mouseTransform - playerTransform).normalized;
        Rigidbody2D fireballRb = fireball.GetComponent<Rigidbody2D>();
        fireballRb.AddForce(direction * this.velocity);
        fireball.gameObject.tag = "basicAttack";

        Destroy(fireball, 0.5f);


    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(fireball);
        }

    }

    private float AngleMath(Vector2 player, Vector2 mouse)
    {
        return Mathf.Atan2(player.y - mouse.y, player.x - mouse.x) * Mathf.Rad2Deg;
    }
}