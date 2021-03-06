﻿using UnityEngine.Networking;
using UnityEngine;

public class HolyWaterCollision : NetworkBehaviour
{
    public float airTime = 2.0f;
    public float startTime;
    public GameObject splashes;
    public int directHeal = 2;

    // Use this for initialization
    void Start()
    {
        startTime = Time.time;

    }

    /*
     * Check if the collision with a player.
     */
    private void OnParticleCollision(GameObject other)
    {
        Destroy(this.gameObject);
        Instantiate(splashes, transform.position, Quaternion.identity);
        SendMessage("Heal", directHeal, SendMessageOptions.DontRequireReceiver);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - startTime > airTime)
        {
            Destroy(this.gameObject);
            Instantiate(splashes, transform.position, Quaternion.identity);
        }
    }

}
