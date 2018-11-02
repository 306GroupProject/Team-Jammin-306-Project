using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class SpikeSpellCooldowns : NetworkBehaviour {

    //public GameObject playerController;

    public Image teleportCooldownImage; // The mask used for the radial cooldown UI
    float teleportCooldown;
    bool teleportCooldownActive;
    bool blocked = false; // Was the teleport blocked by a wall bein in the way? (NOT USED, IN PROGRESS!)
    Teleport teleportScript;

    public Image rockThrowCooldownImage;
    float rockThrowCooldown;
    RockThrow rockThrowScript;
    bool rockThrowCooldownActive;

    void Start()
    {
        

        teleportCooldown = 3;//teleportScript.teleportCooldown;
        rockThrowCooldown = 3;//rockThrowScript.cooldown; // get the cooldown for Spike's RockThrow Ability
    }

    // Update is called once per frame
    void Update () {

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
            rockThrowCooldownActive = true;
        }

        if(rockThrowCooldownActive)
        {
            rockThrowCooldownImage.fillAmount += 1 / rockThrowCooldown * Time.deltaTime;

            if (rockThrowCooldownImage.fillAmount >= 1)
            {
                rockThrowCooldownImage.fillAmount = 0;
                rockThrowCooldownActive = false;
            }
        }

    }

    public void TeleportBlocked(bool isBlocked)
    {
        blocked = isBlocked;
    }

    

}
