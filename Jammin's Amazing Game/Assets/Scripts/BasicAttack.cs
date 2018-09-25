using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : MonoBehaviour {

    //attacks target direction
    private Vector2 target;
    //attack movement speed
    public float speed;
  
	
	void Start () {
        target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
	}
	
	//move to and beyond where the mouse was pressed
	void Update () {
        transform.position = Vector2.MoveTowards(transform.position, target*1000f, speed * Time.deltaTime); 

    
	}
}
