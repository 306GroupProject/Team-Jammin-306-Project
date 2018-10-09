using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CreateAssetMenu(fileName = "Ability", menuName = "Ability")]
public class Ability : Abilities {

    public override void Cast() {
        Debug.Log("Casted " + abilityScript.name);
    }
}
