using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player1" || collision.tag == "Player2" || collision.tag == "Player3" || collision.tag == "Player4")
        {
            collision.GetComponent<playerHealth>().playerHP = 16;
            Destroy(gameObject);
        }
    }
}
