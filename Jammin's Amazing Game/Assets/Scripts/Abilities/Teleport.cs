using UnityEngine;
using UnityEngine.Networking;

public class Teleport : NetworkBehaviour {

    [SyncVar(hook = "MovePlayer")] private Transform playerTransform; // the player's position
    public float teleportCooldown = 3f;
    private float timeSinceTeleport = 0f;
    private Vector2 point; // the point where the mouse is clicked for a teleport
    public GameObject teleportParticles;
    public LayerMask wallMask; // a masking layer for walls that the player CANNOT teleport through
    public float teleportDistance = 10.0f;

    private void Awake() {
        playerTransform = this.transform;
    }

    private void Update() {
        if (isLocalPlayer) {
            // if the player Right Clicks, teleport them to where they clicked.
            if (Input.GetMouseButtonDown(1)) {
                Vector2 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
                RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, teleportDistance, wallMask);
                if (!hit) // if there isn't a wall (gameObject with the tag "Wall") in the way, teleport if the cooldown is over
                {
                    if (timeSinceTeleport <= Time.time) {
                        //cooldownScript.TeleportBlocked(false);
                        Instantiate(teleportParticles, transform.position, Quaternion.identity);
                        point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        transform.position = point;
                        timeSinceTeleport = Time.time + teleportCooldown; // start the cooldown period
                    }
                } else {
                    //cooldownScript.TeleportBlocked(true);
                    Debug.Log("Wall");
                }

            }
        }
    }


    [ClientRpc]
    void RpcTeleport() {
        Instantiate(teleportParticles, transform.position, Quaternion.identity);
        point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = point;
        timeSinceTeleport = Time.time + teleportCooldown; // start the cooldown period
    }

    [Command]
    void CmdTeleport() {
        RpcTeleport();
    }


    void MovePlayer(Transform playerTransform) {
        point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        playerTransform.transform.position = point;
        timeSinceTeleport = Time.time + teleportCooldown; // start the cooldown period
    }

}
