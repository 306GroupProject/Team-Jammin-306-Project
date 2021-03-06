﻿using UnityEngine.Networking;
using UnityEngine;

/**
 * Rock Throw Ability for Spike. Inherits Abilities!
 */ 
public class RockThrow : Abilities {

    private float canAttack;
    /**
     *  Rockthrow ability for Spike, activated using number 1
     */
    public void Update() {
        // Only allow local player to cast, so that other players doesn't cast this ability as well
        if (isLocalPlayer) {
            if (Input.GetKeyDown(KeyCode.Alpha1) && Time.time > canAttack) {
                
                CmdCast(transform.position, (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition));
                canAttack = Time.time + cooldown;
            }
        }
    }

    // Invokes command oclient side, so that method is executed locally!
    [Command]
    void CmdCast(Vector2 playerTransform, Vector2 mouseTransform) {
        RpcCast(playerTransform, mouseTransform);
    }


    // Invokes command server side, so that casting a spell is synchronized across all connected clients!
    [ClientRpc]
    void RpcCast(Vector2 playerTransform, Vector2 mouseTransform) {
        if (!isClient) {
            return;
        }
        // Spawns in a boulder, syncronized accross network!
        GameObject rock = Instantiate(this.projectile, playerTransform, Quaternion.identity);

        Vector2 direction = (mouseTransform - playerTransform).normalized;
        rock.GetComponent<Splatter>().trans = direction;
        Rigidbody2D rockRb = rock.GetComponent<Rigidbody2D>();
        rockRb.AddForce(direction * this.velocity);
        rockRb.AddTorque(100);
       
    }
}
