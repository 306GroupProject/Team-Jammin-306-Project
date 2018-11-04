using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class ZappSpellCooldowns : MonoBehaviour
{

    //public GameObject playerController;

    public Image teleportCooldownImage; // The mask used for the radial cooldown UI
    float teleportCooldown;
    bool teleportCooldownActive;
    bool blocked = false; // Was the teleport blocked by a wall bein in the way? (NOT USED, IN PROGRESS!)
    Teleport teleportScript;

    public Image lightningBoltCooldownImage;
    float lightningBoltCooldown;
    // LightningBolt rockThrowScript;
    bool lightningBoltCooldownActive;

    void Start()
    {


        teleportCooldown = 3;//teleportScript.teleportCooldown;
        lightningBoltCooldown = 3;//rockThrowScript.cooldown; // get the cooldown for Spike's RockThrow Ability
    }

    // Update is called once per frame
    void Update()
    {

        // Code for Teleport (currently mapped to RMB)
        if (!blocked)
        {
            if (Input.GetMouseButtonDown(1)) // if the player right clicks to teleport, activate the teleportCooldown UI
            {
                teleportCooldownActive = true;
            }

            if (teleportCooldownActive)
            {
                teleportCooldownImage.fillAmount += 1 / teleportCooldown * Time.deltaTime;

                if (teleportCooldownImage.fillAmount >= 1) // after the radial animation plays, set everything back to its default state
                {
                    teleportCooldownImage.fillAmount = 0;
                    teleportCooldownActive = false;
                }
            }
        }


        // 
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            lightningBoltCooldownActive = true;
        }

        if (lightningBoltCooldownActive)
        {
            lightningBoltCooldownImage.fillAmount += 1 / lightningBoltCooldown * Time.deltaTime;

            if (lightningBoltCooldownImage.fillAmount >= 1)
            {
                lightningBoltCooldownImage.fillAmount = 0;
                lightningBoltCooldownActive = false;
            }
        }

    }

    public void TeleportBlocked(bool isBlocked)
    {
        blocked = isBlocked;
    }



}
