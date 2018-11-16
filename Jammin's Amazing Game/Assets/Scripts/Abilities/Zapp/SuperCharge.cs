using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SuperCharge : Abilities {

    [SerializeField, SyncVar]
    private float damage = 5.0f;
    private float canAttack;
    public GameObject spark;
    GameObject particle;
    PlayerManager Script;
    int duration = -1;

    void Start()
    {
        InvokeRepeating("charged", 0.0f, 0.1f);
    }

    public void Update()
    {
        // Only allow local player to cast, so that other players doesn't cast this ability as well
        if (isLocalPlayer)
        {
            if (Input.GetKeyDown(KeyCode.Alpha2) && Time.time > canAttack)
            {
                CmdCast(transform.position, (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition));
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
        Script = this.gameObject.GetComponent<PlayerManager>();
        Script.changeSpeed(400.0f);
        duration = 100;
    }

    private void charged()
    {
        if(duration > 0)
        {
            particle = Instantiate(spark, transform.position, Quaternion.identity);
            particle.tag = "Bolt";
            Destroy(particle, 0.5f);
            duration = duration - 1;
        }
        if(duration == 0)
        {
            Script.changeSpeed(100.0f);
            duration = duration - 1;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && duration > 0)
        {
            collision.gameObject.SendMessage("Damage", damage);
        }
    }
}
