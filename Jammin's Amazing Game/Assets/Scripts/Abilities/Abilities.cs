using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public abstract class Abilities : ScriptableObject {

    public string skillName;
    public string description;
    public float manaCost;
    public float cooldown;
    public MonoScript abilityScript;
    public GameObject projectile;

    public abstract void Cast();

}
