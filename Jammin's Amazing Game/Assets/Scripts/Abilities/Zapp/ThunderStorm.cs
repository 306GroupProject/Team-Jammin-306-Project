using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ThunderStorm : Abilities {

    [SerializeField, SyncVar]
    private float canAttack;
    GameObject particle;
    int duration = -1;
    Vector2 mousePosition;

    // Update is called once per frame
    void Update () {
        if (isLocalPlayer)
        {
            if (Input.GetKeyDown(KeyCode.Alpha4) && Time.time > canAttack)
            {
                mousePosition = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
                CmdCast(transform.position, mousePosition);
                canAttack = Time.time + cooldown;
            }
        }
    }

    [Command]
    void CmdCast(Vector2 playerTransform, Vector2 mouseTransform)
    {
        RpcCast(playerTransform, mouseTransform);
    }

    [ClientRpc]
    void RpcCast(Vector2 playerTransform, Vector2 mouseTransform)
    {
        duration = 50;
        InvokeRepeating("storm", 0.0f, 0.2f);
    }

    private void storm()
    {
        if(duration > 0)
        {   
            Vector2 location = mousePosition;
            location.x = Random.Range(mousePosition.x - 5, mousePosition.x + 5);
            location.y = Random.Range(mousePosition.y - 5, mousePosition.y + 5);
            particle = Instantiate(projectile, location, Quaternion.identity);
            particle.tag = "Bolt";
            Destroy(particle, 1.0f);
            duration = duration - 1;
        }
        else
        {
            CancelInvoke();
        }

    }
}
