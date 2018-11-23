using UnityEngine.Networking;
using UnityEngine;

public class SpikeWings : Abilities {

    private float canAttack;
    GameObject particle;
    int duration = -1;
    GameObject wings;
    public int damageMultiplier = 2;

    public int activationTime = 5;
    float timeOut;

    int basicOriginal;
    int boulderOriginal;
    int pebbleOriginal;

    // Save the ability's original damage when wings is casted
    public void Start() {
        InvokeRepeating("Destroy", 0.0f, 0.1f);
        basicOriginal = this.GetComponent<BasicAttack>().projectile.GetComponent<BasicAttackCollision>().damage;
        boulderOriginal = this.GetComponent<BasicAttack>().projectile.GetComponent<BasicAttackCollision>().damage;
        pebbleOriginal = this.GetComponent<RockThrow>().projectile.GetComponent<Splatter>().pebble.GetComponent<Pebble>().damage;
    }

    // Update is called once per frame
    void Update() {
        if (isLocalPlayer) {
            if (Input.GetKeyDown(KeyCode.Alpha3) && Time.time > canAttack) {
                CmdCast(transform.position);
                canAttack = Time.time + cooldown;
            }
        }
    }

    /*
     * Invoke command to server
     */ 
    [Command]
    void CmdCast(Vector2 playerTransform) {
        RpcCast(playerTransform);
    }

    /*
     * Invoke command in client
     */ 
    [ClientRpc]
    void RpcCast(Vector2 playerTransform) {
        wings = Instantiate(projectile, playerTransform, projectile.transform.rotation);
        wings.transform.parent = this.gameObject.transform;
        duration = activationTime * 100;

        this.GetComponent<BasicAttack>().projectile.GetComponent<BasicAttackCollision>().damage *= damageMultiplier;
        this.GetComponent<RockThrow>().projectile.GetComponent<Splatter>().damage *= damageMultiplier;
        this.GetComponent<RockThrow>().projectile.GetComponent<Splatter>().pebble.GetComponent<Pebble>().damage *= damageMultiplier;
    }

    /*
     * Creates particle effects while the ability is active and restores normal speed once the ability expires
     */
    private void Destroy() {
        if (duration > 0) {
            duration = duration - 1;
        }
        if (duration == 0) {
            Destroy(wings.gameObject);
            duration = duration - 1;
            this.GetComponent<BasicAttack>().projectile.GetComponent<BasicAttackCollision>().damage = basicOriginal;
            this.GetComponent<RockThrow>().projectile.GetComponent<Splatter>().damage = boulderOriginal;
            this.GetComponent<RockThrow>().projectile.GetComponent<Splatter>().pebble.GetComponent<Pebble>().damage = pebbleOriginal;
        }

    }
}
