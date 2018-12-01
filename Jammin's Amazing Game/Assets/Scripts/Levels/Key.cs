using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Key : NetworkBehaviour {

    [SerializeField]
    private int keyID; // 0 = key, 1 = key1, 2 = key2

    // option for each gate that can be opened by corresponding keyID
    public GameObject Gate1;
    public GameObject Gate2;
    public GameObject Gate3;
    public GameObject Gate4;
    public GameObject Gate5;
    public GameObject Gate6;
    public GameObject Gate7;


    // when key is collided with corresponding gate is destroyed then key is  
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player1" || other.gameObject.tag == "Player2" || other.gameObject.tag == "Player3" || other.gameObject.tag == "Player4") // can collide with all players
        {
            if (keyID == 0)
            {
                
                Destroy(Gate2);
            }
            
            else if (keyID == 1)
            {
                Destroy(Gate3);
            }
            else if (keyID == 2) // this key brings on a big wave of enemies opening 3 gates
            {
                Destroy(Gate1);
                Destroy(Gate4);
                Destroy(Gate5);
            }
            else if (keyID == 3)
            {
                Destroy(Gate6);
            }
            else if (keyID == 4)
            {
                Destroy(Gate7);
            }

            Destroy(this.gameObject); // key is destroyed
        }
    }
}
