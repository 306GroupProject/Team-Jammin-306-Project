using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    [Range(0.1f, 1.0f)] public float smoothFactor = 0.5f;
    [SerializeField] private Transform playerPosition;
    private Vector3 cameraCoordinates;


	// Use this for initialization
	void Start () {
        transform.position = new Vector3(playerPosition.position.x, playerPosition.position.y, -1);
        cameraCoordinates = transform.position - playerPosition.position;
        Debug.Log(cameraCoordinates);
	}
	
	// Update is called once per frame
	void LateUpdate () {
        transform.position = cameraCoordinates + playerPosition.position;


	}
}
