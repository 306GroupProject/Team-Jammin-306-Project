using UnityEngine;
using UnityEngine.Networking;

public class BasicAttackCollision : NetworkBehaviour {

    public int damage = 1;

    private void OnCollisionEnter2D(Collision2D collision) {
        collision.gameObject.SendMessage("Damage", damage, SendMessageOptions.DontRequireReceiver);
        Destroy(this.gameObject);
    }

}
