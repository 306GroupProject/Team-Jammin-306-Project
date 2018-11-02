using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

// The Cooldown UI for all of Liz's spells and abilities

public class LizzSpellCooldowns : NetworkBehaviour
{

    //public GameObject playerController;

    public Image teleportCooldownImage; // The mask used for the radial cooldown UI
    float teleportCooldown;
    bool teleportCooldownActive;
    bool blocked = false; // Was the teleport blocked by a wall bein in the way? (NOT USED, IN PROGRESS!)
    Teleport teleportScript;

    public Image fireBallCooldownImage;
    float fireBallCooldown;
    FireBall fireBallScript;
    bool fireBallCooldownActive;

    void Start()
    {
        teleportCooldown = 3;//teleportScript.teleportCooldown;
        fireBallCooldown = 2; //rockThrowScript.cooldown; // get the cooldown for Liz's FireBall Ability
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
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            fireBallCooldownActive = true;
        }

        if (fireBallCooldownActive)
        {
            fireBallCooldownImage.fillAmount += 1 / fireBallCooldown * Time.deltaTime;

            if (fireBallCooldownImage.fillAmount >= 1)
            {
                fireBallCooldownImage.fillAmount = 0;
                fireBallCooldownActive = false;
            }
        }

    }

    public void TeleportBlocked(bool isBlocked)
    {
        blocked = isBlocked;
    }



}
