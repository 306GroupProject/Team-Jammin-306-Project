using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SuperCharge : Abilities {

    private float canAttack;
    public GameObject spark;
    GameObject particle;
    PlayerManager Script;
    int duration = -1;

    /**
     *  Fireball ability for Lizz, activated using number 1
     */
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
                Script = this.gameObject.GetComponent<PlayerManager>();
                Script.changeSpeed(200.0f);
                duration = 100;
                canAttack = Time.time + cooldown;
            }
        }
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
}
