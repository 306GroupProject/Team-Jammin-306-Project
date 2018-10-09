using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Abilities : ScriptableObject {

    public string skillName;
    public string description;
    public float manaCost;
    public float cooldown;
    public GameObject projectile;

    public abstract void Cast();

}
