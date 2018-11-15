using UnityEngine.Networking;
using UnityEngine;

public class RockWall : Abilities {

    public float range = 10.0f;
    public float timer = 4.0f;
    private float canAttack;

    void Update () {
		if (isLocalPlayer) {
            if (Input.GetKeyDown(KeyCode.Alpha2) && Time.time > canAttack) {
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
            if (direction.y >= Mathf.Sqrt(2)/2.0f) {
                for (int i = 0; i < 3; i++) {
                    GameObject wall = Instantiate(this.projectile, mouseTransform, Quaternion.identity);
                    mouseTransform.x += wall.transform.localScale.x;
                    Destroy(wall, timer);
                }
            }
            // If the player is shooting behind between within a certain radius, create horizontal wall to the left
            else if (direction.y <= -Mathf.Sqrt(2) / 2.0f) {
                for (int i = 0; i < 3; i++) {
                    GameObject wall = Instantiate(this.projectile, mouseTransform, Quaternion.identity);
                    mouseTransform.x -= wall.transform.localScale.x;
                    Destroy(wall, timer);
                }
            }
            // If the player is shooting right between within a certain radius, create vertical wall upwards
            else if (direction.x >= Mathf.Sqrt(2)/2.0f) {
                for (int i = 0; i < 3; i++) {
                    GameObject wall = Instantiate(this.projectile, mouseTransform, Quaternion.identity);
                    mouseTransform.y += wall.transform.localScale.y;
                    Destroy(wall, timer);
                }
            }
            // If the player is shooting left between within a certain radius, create vertical wall downwards
            else if (direction.x <= -0.7f) {
                for (int i = 0; i < 3; i++) {
                    GameObject wall = Instantiate(this.projectile, mouseTransform, Quaternion.identity);
                    mouseTransform.y -= wall.transform.localScale.y;
                    Destroy(wall, timer);
                }
            }
        }    
    }
}
