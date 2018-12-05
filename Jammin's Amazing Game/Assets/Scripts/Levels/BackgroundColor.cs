using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundColor : MonoBehaviour {

    Color color1 = new Color(0.8207f, 0.2593f, 0.2708f, 1f);
    Color color2 = new Color(0.9339f, 0.6308f, 0.1541f, 1f);
    public Camera cam;

    // Update is called once per frame
    void Update () {
        float time = Mathf.PingPong(Time.time, 4) / 4;
        cam.backgroundColor = Color.Lerp(color1, color2, time);
    }
}
