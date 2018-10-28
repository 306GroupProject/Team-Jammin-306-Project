using UnityEngine;
using UnityEngine.Networking;

public class Meteor : NetworkBehaviour {

    public float velocity = 1000.0f;
    [SerializeField] float airTime = 5.0f;
    [SerializeField, SyncVar] int damageMultiplier = 2;
    [SerializeField] GameObject meteorFragments;
    [SyncVar] Vector2 collidedWithPos;
    Rigidbody2D rb;

    // Update is called once per frame
    void Update () {

       
    }

}
