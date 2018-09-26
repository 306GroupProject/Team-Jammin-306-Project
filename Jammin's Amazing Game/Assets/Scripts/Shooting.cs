using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour {

   
    public GameObject shot;
    private Transform playerPos;

    private float attackRate = 0.50f;
    private float canAttack = 0.0f;


	void Start () {
        //get player position
        playerPos = GetComponent<Transform>();
	}
	
	//fire with left-mouse button
	void Update () {
		if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
	}

    // Instantiate shot at the players current position with a cooldown
    void Shoot()
    {
        if (Time.time > canAttack)
        {
            Instantiate(shot, playerPos.position, Quaternion.identity);
            canAttack = Time.time + attackRate;
        }
    }
}
