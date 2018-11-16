using UnityEngine.Networking;
using UnityEngine;

public class HolyWater : Abilities {

    private float canAttack;
    public float speed = 500.0f;


    /**
     *  Fireball ability for Lizz, activated using number 1
     */
    public void Update() {
        // Only allow local player to cast, so that other players doesn't cast this ability as well
        if (isLocalPlayer) {
            if (Input.GetKeyDown(KeyCode.Alpha2) && Time.time > canAttack) {
                CmdCast(transform.position, (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition));
                canAttack = Time.time + cooldown;
            }
        }
    }

    /*
     * Command to cast across clients so it syncs up with across players.
     */ 
    [Command]
    void CmdCast(Vector2 playerTransform, Vector2 mouseTransform) {
        RpcCast(playerTransform, mouseTransform);
    }

    // Cast the ability server side, so that spell is casted across all connected clients!
    [ClientRpc]
    void RpcCast(Vector2 playerTransform, Vector2 mouseTransform) {
        GameObject holyWater = Instantiate(projectile, playerTransform, Quaternion.identity);
        Vector2 direction = (mouseTransform - playerTransform).normalized;
        holyWater.GetComponent<Rigidbody2D>().AddForce(speed * direction);

    }

}
