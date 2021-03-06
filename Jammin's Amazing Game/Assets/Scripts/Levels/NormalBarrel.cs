﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NormalBarrel : NetworkBehaviour {

    // simple obstacles destroyed by anyone's basic attacks
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "basicAttack" || other.gameObject.layer == 13)
        {
            Destroy(this.gameObject);
        }
    }

}
