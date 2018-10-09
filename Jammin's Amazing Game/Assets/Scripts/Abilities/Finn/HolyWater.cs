using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Abilities/Finn/Holy Water")]
public class HolyWater : Abilities {
    
    public override void Cast() {
        Debug.Log("Casted Holy Water!");
    }
}
