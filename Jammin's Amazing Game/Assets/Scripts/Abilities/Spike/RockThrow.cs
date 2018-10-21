﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Rock Throw Ability for Spike. Inherits Abilities!
 */ 
public class RockThrow : Abilities {


    /**
     *  Rockthrow ability for Spike, activated using number 1
     */
    public void Update() {
        // Only allow local player to cast, so that other players doesn't cast this ability as well
        if (isLocalPlayer) {
            if (Input.GetKeyDown(KeyCode.Alpha1)) {
                CmdCast(transform.position, (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition));
            }
        }
    }


    public override void CmdCast(Vector2 playerTransform, Vector2 mouseTransform) {
        RpcCast(playerTransform, mouseTransform);
    }

    public override void RpcCast(Vector2 playerTransform, Vector2 mouseTransform) {
        // Spawns in a boulder, syncronized accross network!
        GameObject rock = Instantiate(this.projectile, playerTransform, Quaternion.identity);
        Vector2 direction = (mouseTransform - playerTransform).normalized;
        Rigidbody2D rockRb = rock.GetComponent<Rigidbody2D>();
        rockRb.AddForce(direction * this.velocity);
        rockRb.AddTorque(100);
       
    }
}
