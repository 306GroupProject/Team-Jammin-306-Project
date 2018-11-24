using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/*
 * Handles Electric Puddle Collision
 */
public class EPuddleCollison : NetworkBehaviour
{
	[SerializeField, SyncVar]
	private int damage = 1;
	
	public float dotRate = 1.0f;
	float startTime;
	public bool isThisBossMan = false; 
	
	
	public void Awake() {
		startTime = Time.time;
	}
	
	public void OnTriggerStay2D(Collider2D collision) {
		if ((collision.gameObject.GetComponent ("ai") as ai) != null) {
			// For the time duration an enemy is inside electric puddle's trigger, send damage 
			// messaage to enemy once every 1 dotRateSeconds.
			if (Time.time > startTime) {
				collision.gameObject.SendMessage ("Damage", damage);
				startTime = Time.time + dotRate;
			}
		} else if (isThisBossMan == true) {
			
			
			if(collision.gameObject.tag.Equals("Player1")|| collision.gameObject.tag.Equals("Player2")|| collision.gameObject.tag.Equals("Player3")|| collision.gameObject.tag.Equals("Player4") ){
				
				if (Time.time > startTime) {
					collision.gameObject.SendMessage ("Damage", damage);
					startTime = Time.time + dotRate;
				}
				
			}
			
			isThisBossMan = false; 
			
		}
	}
	
	
}
