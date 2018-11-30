using UnityEngine.Networking;
using UnityEngine;

public class BasicAttack : Abilities {

    float canAttack;
    [SerializeField] float airTime = 1.0f;


    /*
     * Allows the player to cast basic attack every cooldown seconds.
     */ 
    public void Update() {
        if (isLocalPlayer) {
            if (Input.GetMouseButton(0) && Time.time > canAttack) {
                this.CmdCast(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
                canAttack = Time.time + cooldown;
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

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // Rotate the sprite
        bullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        Destroy(bullet, airTime);  // Destroy bullet after 1s midair!
    }


}
