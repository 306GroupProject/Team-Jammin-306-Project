using UnityEngine.Networking;
using UnityEngine;

public class FlamethrowerCollision : MonoBehaviour {

    ParticleSystem flames;  // We need to access the particle effect attached to this to access collisions with it.
    public int damageOverTime = 2;
    public float damageRate = 1.0f;
    public GameObject flameWall;
    public GameObject tarFire;
    float startTime;

    private void Start() {
        startTime = Time.time;
        flames = GetComponent<ParticleSystem>();
    }

    /*
     * Check if the instantiated particle representing the flame thrower collided with the  object
     */ 
    private void OnParticleCollision(GameObject other) {
        if(other.tag == "Tar") {
            Destroy(other.gameObject);
            GameObject fire = Instantiate(tarFire, other.transform.position, Quaternion.identity);
            Destroy(fire.gameObject, tarFire.GetComponent<ParticleSystem>().main.duration);
            Destroy(other.gameObject, 2);
            Destroy(this.gameObject);
        }

        else if (other.tag == "Enemy") {
            if (Time.time > startTime) {
                other.gameObject.SendMessage("Damage", damageOverTime);
                startTime = Time.time + damageRate;
            }
        }
        // Wall + Flame Thrower combination
        else if (other.tag == "RockWall") {
            Destroy(this.gameObject);
            GameObject fireWall = Instantiate(flameWall, other.gameObject.transform.position, other.gameObject.transform.rotation);
            // If the wall is laid out vertically
            if (other.transform.rotation.z > 0) {
                /*
                 * These series of flamses instantiates surrounding fire around rock wall to match the wall's pattern.
                 * It still does Damage to enmies, and can spread to nearby walls (vertical)
                 */ 
                GameObject flame = Instantiate(flames.gameObject, fireWall.transform.position - Vector3.down, Quaternion.Euler(new Vector3(-90,-90,0)));

                GameObject flame1 = Instantiate(flames.gameObject, fireWall.transform.position - Vector3.up, Quaternion.Euler(new Vector3(90, 90, 0)));
                flame1.transform.localScale = new Vector3(flame1.transform.localScale.x, flame1.transform.localScale.y, flame1.transform.localScale.z);

                GameObject flame2 = Instantiate(flames.gameObject, fireWall.transform.position - (Vector3.right * 2), Quaternion.Euler(new Vector3(90, 90, 0)));
                flame2.transform.localScale = new Vector3(flame2.transform.localScale.x, flame2.transform.localScale.y, -flame2.transform.localScale.z);

                GameObject flame3 = Instantiate(flames.gameObject, fireWall.transform.position - (Vector3.right * 2), Quaternion.Euler(new Vector3(-90, 90, 0)));
                flame3.transform.localScale = new Vector3(flame3.transform.localScale.x, flame3.transform.localScale.y, -flame3.transform.localScale.z);

                GameObject flame4 = Instantiate(flames.gameObject, fireWall.transform.position - (Vector3.left * 2), Quaternion.Euler(new Vector3(90, 90, 0)));
                flame4.transform.localScale = new Vector3(flame4.transform.localScale.x, flame4.transform.localScale.y, flame4.transform.localScale.z);

                GameObject flame5 = Instantiate(flames.gameObject, fireWall.transform.position - (Vector3.left * 2), Quaternion.Euler(new Vector3(-90, 90, 0)));
                flame5.transform.localScale = new Vector3(flame5.transform.localScale.x, flame5.transform.localScale.y, flame5.transform.localScale.z);

                Destroy(flame, flame.GetComponent<ParticleSystem>().main.duration / 2);
                Destroy(flame1, flame1.GetComponent<ParticleSystem>().main.duration / 2);
                Destroy(flame2, flame2.GetComponent<ParticleSystem>().main.duration / 2);
                Destroy(flame3, flame3.GetComponent<ParticleSystem>().main.duration / 2);
                Destroy(flame4, flame4.GetComponent<ParticleSystem>().main.duration / 2);
                Destroy(flame5, flame5.GetComponent<ParticleSystem>().main.duration / 2);
            } 
            // Otherwise, if the wall is laid out horizontally
            else {
                /*
                 * These series of flamses instantiates surrounding fire around rock wall to match the wall's pattern.
                 * It still does Damage to enmies, and can spread to nearby walls (horizontal)
                 */
                GameObject flame = Instantiate(flames.gameObject, fireWall.transform.position - Vector3.left, Quaternion.Euler(new Vector3(0, 90,0)));

                GameObject flame1 = Instantiate(flames.gameObject, fireWall.transform.position - Vector3.right, Quaternion.Euler(new Vector3(0, 90, 0)));
                flame1.transform.localScale = new Vector3(flame1.transform.localScale.x, flame1.transform.localScale.y,-flame1.transform.localScale.z);

                GameObject flame2 = Instantiate(flames.gameObject, fireWall.transform.position - (Vector3.down * 2), Quaternion.Euler(new Vector3(0, 90, 0)));
                flame2.transform.localScale = new Vector3(flame1.transform.localScale.x, flame2.transform.localScale.y, -flame1.transform.localScale.z);

                GameObject flame3 = Instantiate(flames.gameObject, fireWall.transform.position - (Vector3.down* 2), Quaternion.Euler(new Vector3(0, -90, 0)));

                GameObject flame4 = Instantiate(flames.gameObject, fireWall.transform.position - (Vector3.up * 2), Quaternion.Euler(new Vector3(0, 90, 0)));
                flame4.transform.localScale = new Vector3(flame1.transform.localScale.x, flame2.transform.localScale.y, -flame1.transform.localScale.z);

                GameObject flame5 = Instantiate(flames.gameObject, fireWall.transform.position - (Vector3.up * 2), Quaternion.Euler(new Vector3(0, -90, 0)));
                flame5.transform.localScale = new Vector3(flame1.transform.localScale.x, flame2.transform.localScale.y, -flame1.transform.localScale.z);

                /*
                 * Destroy flames particles after play time endsS
                 */ 
                Destroy(flame, flame.GetComponent<ParticleSystem>().main.duration / 2);
                Destroy(flame1, flame1.GetComponent<ParticleSystem>().main.duration / 2);
                Destroy(flame2, flame2.GetComponent<ParticleSystem>().main.duration / 2);
                Destroy(flame3, flame3.GetComponent<ParticleSystem>().main.duration / 2);
                Destroy(flame4, flame4.GetComponent<ParticleSystem>().main.duration / 2);
                Destroy(flame5, flame5.GetComponent<ParticleSystem>().main.duration / 2);
                
            }
            Destroy(fireWall, flames.GetComponent<ParticleSystem>().main.duration / 2);
            Destroy(other.gameObject);
        }
    }
}
