using UnityEngine;
using UnityEngine.Networking;


/**
 * Fireball Ability for Lizz. Inherits Abilities!
 */
public class FireBall : Abilities
{
    
    private float canAttack;
    GameObject fireball;
    public GameObject barrelExplosion;

    /**
     *  Fireball ability for Lizz, activated using number 1
     */
    public void Update()
    {
        // Only allow local player to cast, so that other players doesn't cast this ability as well
        if (isLocalPlayer)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && Time.time > canAttack)
            {
                CmdCast(transform.position, (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition));
                canAttack = Time.time + cooldown;
            }
        }
    }

    
    [Command]
    void CmdCast(Vector2 playerTransform, Vector2 mouseTransform)
    {
        RpcCast(playerTransform, mouseTransform);
    }

    // Cast the ability server side, so that spell is casted across all connected clients!
    [ClientRpc]
     void RpcCast(Vector2 playerTransform, Vector2 mouseTransform)
    {
        // Spawns in a fireball, syncronized accross network!
        float angle = AngleMath(playerTransform, mouseTransform) + 90.0f;

        fireball = Instantiate(this.projectile, playerTransform, Quaternion.Euler(new Vector3(0f, 0f, angle)));
        Vector2 direction = (mouseTransform - playerTransform).normalized;
   
        fireball.GetComponent<Rigidbody2D>().AddForce(direction * this.velocity);

        
    }


    private float AngleMath(Vector2 player, Vector2 mouse)
    {
        return Mathf.Atan2(player.y - mouse.y, player.x - mouse.x) * Mathf.Rad2Deg;
    }
}