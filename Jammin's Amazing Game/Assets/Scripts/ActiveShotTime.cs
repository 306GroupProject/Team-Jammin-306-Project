using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveShotTime : MonoBehaviour {

    //destroy an attack after set time
    public float shotTime = 0.02f;

	void Start () {
        Destroy(gameObject, shotTime);
	}

}
