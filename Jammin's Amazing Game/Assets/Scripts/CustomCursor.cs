using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCursor : MonoBehaviour {

    public Texture2D cursor;
    public GameObject particleSprite;
    public float followSpeed = 1.0f;
   
    
	// Use this for initialization
	void Start () {
        particleSprite.transform.position = transform.position;
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 mousePosition = Input.mousePosition;
        
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        transform.position = Vector2.Lerp(transform.position, mousePosition, followSpeed);
    }
}
