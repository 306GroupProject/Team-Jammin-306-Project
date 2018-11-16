using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


/**
 * Lightning Ability for Zapp. Inherits Abilities!
 */
public class Lightning : Abilities
{
    public float lifeTime = 1.0f;
    private float canAttack;
    GameObject bolt;
    public int iteration = -1;
    float angle;
    Vector2 direction;
    Vector2 newPosition;


    void Start()
    {
        InvokeRepeating("lightningStrike", 0.0f, 0.01f);
    }
    /**
     *  Lightning ability for Zapp, activated using number 1
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
        // Spawns in a bolt, syncronized accross network!
        iteration = 0;
        direction = (mouseTransform - playerTransform).normalized * 40;
        newPosition = playerTransform;
        angle = AngleMath(playerTransform, mouseTransform) + 90.0f;
        iteration = 10;
        /*
        while (iteration < 15)
        {
            bolt = Instantiate(this.projectile, newPosition, Quaternion.Euler(new Vector3(0f, 0f, angle)));
            newPosition = (Vector2)bolt.transform.position + direction / 15;
            iteration++;
            Destroy(bolt, lifeTime);
        }
        */
    }

    private void lightningStrike()
    {
        if (iteration > 0)
        {
            bolt = Instantiate(this.projectile, newPosition, Quaternion.Euler(new Vector3(0f, 0f, angle)));
            newPosition = (Vector2)bolt.transform.position + direction / 15;
            iteration--;
            Destroy(bolt, lifeTime);
        }
    }

    private float AngleMath(Vector2 player, Vector2 mouse)
    {
        return Mathf.Atan2(player.y - mouse.y, player.x - mouse.x) * Mathf.Rad2Deg;
    }
}
