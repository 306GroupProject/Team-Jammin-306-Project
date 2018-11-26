using UnityEngine;
using UnityEngine.Networking;

/*
 * Meteor Script that handles meteor collisions.
 */ 
public class Meteor : NetworkBehaviour {
	
	public float velocity = 500.0f;     
	[SerializeField] float airTime = 5.0f;
	
	int damageMultiplier = 2;       // Multiply damage of meteor by 2 * fireball damage.
	public int damage;
	
	[SerializeField] GameObject meteorFragments;
	public int spreadRate = 4;
	public float fragForce = 300.0f;
	
	// Diagonal trajectories
	static Vector2 diagLeft = new Vector2(-Mathf.Sqrt(2) / 2, Mathf.Sqrt(2) / 2);
	static Vector2 diagRight = new Vector2(Mathf.Sqrt(2) / 2, -Mathf.Sqrt(2) / 2);
	static Vector2 diagDLeft = new Vector2(-Mathf.Sqrt(2) / 2, -Mathf.Sqrt(2) / 2);
	static Vector2 diagDRight = new Vector2(Mathf.Sqrt(2) / 2, Mathf.Sqrt(2) / 2);
	
	Vector2[] crossPositions = { Vector2.up, diagLeft, Vector2.left, diagDLeft, Vector2.down, diagRight, Vector2.right, diagDRight };
	
	float startTime;
	public GameObject explosion;
	
	// Rotation where the meteor fragment should be facing.
	float[] rotations = { 0, 45.0f, 90.0f, 135.0f, 180.0f, 225.0f, 270.0f, 315.0f };
	
	
	public bool isThisBossMan = false; 
	
	
	/*
     * Sets up player damage by getting the Synced damage value of meteor, by the fireball damage
     */
	public void Awake() {
		startTime = Time.time;
		damage *= damageMultiplier;
	}
	
	// Keep track of meteor's airtime
	void Update () {
		if (Time.time - startTime > airTime) {
			Vector2 save = transform.position;
			Destroy(this.gameObject);
			GameObject explode = Instantiate(explosion, save, Quaternion.identity);
			Destroy(explode, 2.0f);
		}
	}
	
	
	private void OnCollisionEnter2D(Collision2D collision) {
		// If the meteor collides with the enemy, send a damage message to inflict damage to enemy.
		
		if (collision.gameObject.tag == "Enemy" || collision.gameObject.layer == 15) {
			collision.gameObject.SendMessage ("Damage", damage, SendMessageOptions.DontRequireReceiver);
			
		} else if (isThisBossMan == true) { // this is the case were the caster is the boss and not a player.
			Physics2D.IgnoreCollision (collision.gameObject.GetComponent<BoxCollider2D> (), this.gameObject.GetComponent<CircleCollider2D> (), false); 
			
            // Do individual checks for each meteor collision. we need to check if the meteor collider collides
            // with a player. If so, send a damage message.
			if (collision.gameObject.tag.Equals ("Player1")) {
				
				
				collision.gameObject.SendMessage ("Damage", damage);
			}
            if (collision.gameObject.tag.Equals("Player2")) {


                collision.gameObject.SendMessage("Damage", damage);
            }
            if (collision.gameObject.tag.Equals("Player3")) {


                collision.gameObject.SendMessage("Damage", damage);
            }
            if (collision.gameObject.tag.Equals("Player4")) {


                collision.gameObject.SendMessage("Damage", damage);
            }

            isThisBossMan = false; 
			
		} else {
			// Otherwise, if the meteor collides with anything else, spawn meteor frags in a circular projectile.
			for (int i = 0; i < crossPositions.Length; i++) {
				GameObject fragements = Instantiate(meteorFragments, transform.position, Quaternion.Euler(new Vector3(0, 0, rotations[i])));
				fragements.GetComponent<Rigidbody2D>().AddForce(crossPositions[i] * fragForce);
				
			}
		}
		Vector2 save = transform.position;
		Destroy(this.gameObject);
		// Player explosion particle effect.
		GameObject explode = Instantiate(explosion, save, Quaternion.identity);
		Destroy(explode, 2.0f);
	}
	
}

