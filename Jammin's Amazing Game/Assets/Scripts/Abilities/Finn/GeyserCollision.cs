using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeyserCollision : MonoBehaviour
{
    public GameObject particle;

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bolt")
        {
            InvokeRepeating("charged", 0.0f, 0.2f);
        }
    }

    void charged()
    {
        particle = Instantiate(particle, transform.position, Quaternion.identity);
        particle.tag = "Bolt";
        Destroy(particle, 0.5f);
    }
}