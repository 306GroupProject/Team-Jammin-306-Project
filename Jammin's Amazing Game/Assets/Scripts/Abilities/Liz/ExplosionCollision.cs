﻿using UnityEngine.Networking;
using UnityEngine;

public class ExplosionCollision : NetworkBehaviour {


    public GameObject meteor;
    float canDamage;
    public float damageRate;
    public int damageTick;
    float startTime;
    Vector2[] positions = { Vector2.left, Vector2.right, Vector2.up, Vector2.down };
    float[] rotations = { 90.0f, -90.0f, 0.0f, 180.0f };

    // Trajectory directions



    /*
     * Checks particle collision to handle enemy damage.
     */ 
    private void OnParticleCollision(GameObject collision) {
        if (collision.gameObject.tag == "Enemy" && Time.time > canDamage) {
            canDamage = Time.time + damageRate;
            collision.SendMessage("Damage", damageTick, SendMessageOptions.DontRequireReceiver);
        } else if ((collision.gameObject.tag == "Player1" || collision.gameObject.tag == "Player2" 
            || collision.gameObject.tag == "Player4") && Time.time > canDamage) {
            canDamage += Time.time + damageRate + 2;
            collision.SendMessage("Damage", 1, SendMessageOptions.DontRequireReceiver);
        }

        // If explosion hits a boulder, combine!
        if (collision.gameObject.tag == "Boulder") {

            Vector2 spawnMeteorPoint = collision.gameObject.transform.position;

            // Spawns 4 meteors in cross-shaped pattern with             
            for (int i = 0; i < 4; i++) {
                Vector2 direction = positions[i];
                Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, rotations[i]));
                GameObject fire = Instantiate(meteor, spawnMeteorPoint, rotation);
                fire.GetComponent<Rigidbody2D>().AddForce(direction.normalized * fire.GetComponent<Meteor>().velocity);
                meteor.GetComponent<Meteor>().damage = this.damageTick;
            }
            Destroy(collision.gameObject);
        }
    }
}
