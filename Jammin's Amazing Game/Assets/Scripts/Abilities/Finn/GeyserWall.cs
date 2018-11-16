using UnityEngine.Networking;
using UnityEngine;

public class GeyserWall : Abilities {

    public float range = 5000.0f;
    public float timer = 2.0f;
    public float speed = 1000.0f;
    private float canAttack;

    void Update() {
        if (isLocalPlayer) {
            if (Input.GetKeyDown(KeyCode.Alpha3) && Time.time > canAttack) {
                CmdCast(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
                canAttack = Time.time + cooldown;
            }
        }
    }

    // Invokes command oclient side, so that method is executed locally!
    [Command]
    void CmdCast(Vector2 playerTransform, Vector2 mouseTransform) {
        RpcCast(playerTransform, mouseTransform);
    }


    // Invokes command server side, so that casting a spell is synchronized across all connected clients!
    [ClientRpc]
    void RpcCast(Vector2 playerTransform, Vector2 mouseTransform) {
        if (!isClient) {
            return;
        }

        // If the mouse cursor is witin range of the player
        if (Vector2.Distance(playerTransform, mouseTransform) <= range) {
            // Get the direction the player should shoot towards
            Vector2 direction = (mouseTransform - playerTransform).normalized;

            // If the player is shooting forward between within a certain radius, create horizontal wall to the right
            if (direction.y >= Mathf.Sqrt(3) / 2.0f || direction.y <= -Mathf.Sqrt(3) / 2.0f) {
                GameObject geyser = Instantiate(this.projectile, playerTransform, Quaternion.Euler(new Vector3(0,0,90)));
                geyser.GetComponent<Rigidbody2D>().AddForce(direction * speed);
                Destroy(geyser, timer);
            }
            // If the player is shooting right between within a certain radius, create vertical wall upwards
            else if (direction.x >= Mathf.Sqrt(3) / 2.0f || direction.x <= -Mathf.Sqrt(3) / 2.0f) {
                GameObject geyser = Instantiate(this.projectile, playerTransform, Quaternion.identity);
                geyser.GetComponent<Rigidbody2D>().AddForce(direction * speed);
                Destroy(geyser, timer);               
            }
            // Check if player is pointing at NE or SW positions, for rotating wall in appropriate position.
            else if (((direction.x < Mathf.Sqrt(3) / 2.0f && direction.x > 1.0f/2.0f) && 
                       (direction.y <= Mathf.Sqrt(3) / 2.0f && direction.y >= 1.0f / 2.0f)) || 
                       ((direction.x > -Mathf.Sqrt(3) / 2.0f && direction.x < -1.0f / 2.0f) &&
                       (direction.y >= -Mathf.Sqrt(3) / 2.0f && direction.y <= -1.0f / 2.0f))) {
                GameObject geyser = Instantiate(this.projectile, playerTransform, Quaternion.Euler(new Vector3(0, 0, 45)));
                geyser.GetComponent<Rigidbody2D>().AddForce(direction * speed);
                Destroy(geyser, timer);
            } 
            // Check if player is pointing at NW or SE direction.
            else {
                GameObject geyser = Instantiate(this.projectile, playerTransform, Quaternion.Euler(new Vector3(0, 0, -45)));
                geyser.GetComponent<Rigidbody2D>().AddForce(direction * speed);
                Destroy(geyser, timer);
            }

        }
    }
}
