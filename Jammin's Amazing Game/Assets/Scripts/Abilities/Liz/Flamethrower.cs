using UnityEngine;
using UnityEngine.Networking;


/*
 * Fireball Ability for Lizz. Inherits Abilities!
 */
public class Flamethrower : Abilities {

    private float fireDuration;
    private float canAttack;
    GameObject fire;

    private void Start() {
        fireDuration = projectile.GetComponent<ParticleSystem>().main.duration / 2;
    }

    /*
     *  Fireball ability for Lizz, activated using number 1
     */
    public void Update() {
        // Only allow local player to cast, so that other players doesn't cast this ability as well
        if (isLocalPlayer) {
            
            if (Input.GetKeyDown(KeyCode.Alpha2) && Time.time > canAttack) {
                CmdCast(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
                canAttack = Time.time + cooldown;
            }           
            CmdSyncRotate(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));          
        }
    }


    [Command]
    void CmdCast(Vector2 playerTransform, Vector2 mouseTransform) {
        RpcCast(playerTransform, mouseTransform);
    }

    // Cast the ability server side, so that spell is casted across all connected clients!
    [ClientRpc]
    void RpcCast(Vector2 playerTransform, Vector2 mouseTransform) {
        canAttack = Time.time + cooldown;
        Vector2 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
        float fireTowards = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        fire = Instantiate(this.projectile, transform.position, Quaternion.Euler(new Vector3(-fireTowards, 90, 90)));
    }

    // Sync rotation across clients!
    [Command]
    void CmdSyncRotate(Vector2 playerTransform, Vector2 mouseTransform) {
        RpcSyncRotate(playerTransform, mouseTransform);
    }

    // This method ensures that the direction and position of the flamethrower is synced across connected
    // players.
    [ClientRpc]
    void RpcSyncRotate(Vector2 playerTransform, Vector2 mouseTransform) {
        if (fire != null) {
            Vector2 direction = (mouseTransform - playerTransform).normalized;
            fire.transform.position = playerTransform;
            float fireTowards = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            fire.transform.rotation = Quaternion.Euler(new Vector3(-fireTowards, 90, 90));
            Destroy(fire, fireDuration);
        }
    }
}