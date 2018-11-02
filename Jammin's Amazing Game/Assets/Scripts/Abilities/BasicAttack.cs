using UnityEngine.Networking;
using UnityEngine;

public class BasicAttack : Abilities {

    float startTime;
    [SerializeField] float airTime = 1.0f;

    /*
     * When script is active, begin the timer
     */ 
    public void Awake() {
        startTime = Time.time;
    }

    /*
     * Allows the player to cast basic attack every cooldown seconds.
     */ 
    public void Update() {
        if (isLocalPlayer) {
            if (Input.GetMouseButtonDown(0) && this.cooldown + startTime < Time.time) {
                this.CmdCast(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
                startTime = Time.time;
            }
            
        }
    }

    /*
     * Cast the spell Server side, so it syncs up other players!
     */ 
    [Command]
    void CmdCast(Vector2 playerTransform, Vector2 mouseTransform) {
        this.RpcCast(playerTransform, mouseTransform);
    }

    /*
     * Cast the spell client side. 
     */ 
    [ClientRpc]
    void RpcCast(Vector2 playerTransform, Vector2 mouseTransform) {
        Vector2 direction = (mouseTransform - playerTransform).normalized;
        GameObject bullet = Instantiate(this.projectile, playerTransform, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().AddForce(this.velocity * direction);
        Destroy(bullet, airTime);  // Destroy bullet after 1s midair!
    }


}
