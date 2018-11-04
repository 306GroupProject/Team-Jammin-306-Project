using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

/**
 * Puddle Ability for Finn. Inherits Abilities!
 */
public class Puddle : Abilities
{
    public float lifeTime = 10.0f;
    private float canAttack;
    GameObject puddle;

    /**
     *  Puddle ability for Finn, activated using number 1
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

    [ClientRpc]
    void RpcCast(Vector2 playerTransform, Vector2 mouseTransform)
    {
        // Spawns in a puddle, syncronized accross network!

        puddle = Instantiate(this.projectile, mouseTransform, Quaternion.identity);

        Destroy(puddle, lifeTime);
        PlayerManager Script = this.gameObject.GetComponent<PlayerManager>();
        Script.changeSpeed(100.0f);
    }
}