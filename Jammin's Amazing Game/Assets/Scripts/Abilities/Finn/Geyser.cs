using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Abilities/Finn/Geyser")]
public class Geyser : Abilities {

    public override void Cast() {
        Debug.Log("Casted Geyser");
    }

}
