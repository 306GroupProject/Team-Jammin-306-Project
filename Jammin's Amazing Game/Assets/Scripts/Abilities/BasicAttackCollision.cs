﻿using UnityEngine;
using UnityEngine.Networking;

public class BasicAttackCollision : NetworkBehaviour {

    [SerializeField] int damage = 1;

    private void OnCollisionEnter2D(Collision2D collision) {
        collision.gameObject.SendMessage("Damage", 1, SendMessageOptions.DontRequireReceiver);
        Destroy(this.gameObject);
    }

}