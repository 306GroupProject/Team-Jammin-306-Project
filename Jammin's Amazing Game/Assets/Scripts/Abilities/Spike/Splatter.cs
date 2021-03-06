﻿using UnityEngine;
using UnityEngine.Networking;

/**
 * Splatter is a class that controls rockThrow collisions and interactions 
 * after being casted 
 */
public class Splatter : NetworkBehaviour {

    [SyncVar] public int damage = 4;

    public float airtime;
    public int splashRate;
    public GameObject pebble;
    public GameObject smoke;

    [HideInInspector] public Vector2 trans;
    [SerializeField] GameObject meteor;

    float startTime;
    Vector2[] positions = { Vector2.left, Vector2.right, Vector2.up, Vector2.down};
    float[] rotations = { 90.0f, -90.0f, 0.0f, 180.0f };

    // Trajectory directions
    static Vector2 diagLeft = new Vector2(-Mathf.Sqrt(2) / 2, Mathf.Sqrt(2) / 2);
    static Vector2 diagRight = new Vector2(Mathf.Sqrt(2) / 2, -Mathf.Sqrt(2) / 2);
    static Vector2 diagDLeft = new Vector2(-Mathf.Sqrt(2) / 2, -Mathf.Sqrt(2) / 2);
    static Vector2 diagDRight = new Vector2(Mathf.Sqrt(2) / 2, Mathf.Sqrt(2) / 2);

    Vector2[] crossPositions = { Vector2.up, diagLeft, Vector2.left, diagDLeft, Vector2.down, diagRight, Vector2.right, diagDRight };

    /*
     * Awake starts our timer for destroying projectile mid-air
     */
    private void Awake() {
        startTime = Time.time;
    }

    /*
     * Update keeps track of our boulder's airtime. If it is destroyed mid air, spawn
     * mini-projectiles that deals small damage to enemies.
     */ 
    public void Update() {
        // Destroy the casted rock throw after X amount of seconds.
        if (Time.time - startTime > airtime) {
            // Loops that spawns out the projectiles after being destroyed mid-air.
            for (int i = 0; i < crossPositions.Length; i++) {
                GameObject pebbles = Instantiate(pebble, transform.position, Quaternion.identity);
                pebbles.GetComponent<Pebble>().CmdCast(transform.position, crossPositions[i], 500.0f);
            }
            
            Vector2 save = transform.position;
            
            Destroy(this.gameObject);
            
            GameObject smoker = Instantiate(smoke, save, Quaternion.identity);
            Destroy(smoker.gameObject, 1.0f);

        }
    }

    /*
     * Collision detector
     */ 
    public void OnCollisionEnter2D(Collision2D collision) {
        // Don't spawn in pebbles if collides with a fireball. Will summon meteors!
        if (collision.gameObject.tag == "Fireball" || collision.gameObject.tag == "Explosion") {
            Vector2 spawnMeteorPoint = collision.gameObject.transform.position;  

            // Spawns 4 meteors in cross-shaped pattern with             
            for (int i = 0; i < 4; i++) {
                Vector2 direction = positions[i];
                Quaternion rotation = Quaternion.Euler(new Vector3(0,0,rotations[i]));
                GameObject fire = Instantiate(meteor, spawnMeteorPoint, rotation);
                fire.GetComponent<Rigidbody2D>().AddForce(direction.normalized * fire.GetComponent<Meteor>().velocity);
                meteor.GetComponent<Meteor>().damage = this.damage;
            }

        } else if (collision.gameObject.tag == "Enemy" || collision.gameObject.layer == 15) {


            collision.gameObject.SendMessage("Damage", damage, SendMessageOptions.DontRequireReceiver);
        }

       else {
            // Sends damage message to the object rock collided with
            
            // Splash pebbles.
            for (int i = 0; i < crossPositions.Length; i++) {

                GameObject pebbles = Instantiate(pebble, transform.position, Quaternion.identity);
                pebbles.GetComponent<Pebble>().CmdCast(transform.position, crossPositions[i], 500.0f);
            }
        }
        Vector2 save = transform.position;

        Destroy(this.gameObject);

        GameObject smoker = Instantiate(smoke, save, Quaternion.identity);
        Destroy(smoker.gameObject, 1.0f);
    }

    


}
