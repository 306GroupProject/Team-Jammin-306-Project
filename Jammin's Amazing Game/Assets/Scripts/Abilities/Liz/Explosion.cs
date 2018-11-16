using UnityEngine.Networking;
using UnityEngine;

public class Explosion : Abilities {

    float canAttack;
 
    public void Update() {
        if (isLocalPlayer) {
            if (Input.GetKeyDown(KeyCode.Alpha3) && Time.time > canAttack) {
                CmdCast(transform.position);
                canAttack = Time.time + cooldown;
            }
        }
    }

    /*
    * Command to cast across clients so it syncs up with across players.
    */
    [Command]
    void CmdCast(Vector2 playerTransform) {
        RpcCast(playerTransform);
    }

    // Cast the ability server side, so that spell is casted across all connected clients!
    [ClientRpc]
    void RpcCast(Vector2 playerTransform) {
        GameObject explosion = Instantiate(projectile, playerTransform, Quaternion.identity);
        Destroy(explosion, explosion.gameObject.GetComponent<ParticleSystem>().main.duration / 2);

    }
}
