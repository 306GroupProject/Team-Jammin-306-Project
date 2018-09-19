using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A simple script which makes a gameObject follow the player's mouse cursor
public class FollowMousePosition : MonoBehaviour {
  
    public float followSpeed = 0.1f; // admittedly it may be better to just directly set the position to that of the mouse, but the smoothing offered by Lerp makes it look a little nicer
    public GameObject cursor;


    void Start() {
        cursor = GetComponent<GameObject>();
       
    }


    // Update is called once per frame
    void Update () {
        Vector2 mousePosition = Input.mousePosition;

        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        transform.position = Vector2.Lerp(transform.position, mousePosition, followSpeed);
    }
}
