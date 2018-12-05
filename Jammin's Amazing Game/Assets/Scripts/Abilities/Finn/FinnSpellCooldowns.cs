
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class FinnSpellCooldowns : MonoBehaviour
{
    // Teleport
    public Image teleportCooldownImage; // The mask used for the radial cooldown UI
    float teleportCooldown;
    bool teleportCooldownActive;
    bool blocked = false; // Was the teleport blocked by a wall being in the way?
    Teleport teleportScript;

    // Puddle
    public Image puddleCooldownImage;
    float puddleCooldown;
    bool puddleCooldownActive;

    // Holy Water
    public Image holyWaterCooldownImage;
    float holyWaterCooldown;
    bool holyWaterCooldownActive;

    // Geyser Wall
    public Image geyserWallCooldownImage;
    float geyserWallCooldown;
    bool geyserWallCooldownActive;

    void Start()
    {
        
        teleportCooldown = 3;
        puddleCooldown = 6;
        holyWaterCooldown = 4;
        geyserWallCooldown = 3;
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


        // Code for Puddle (currently mapped to 1)
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            puddleCooldownActive = true;
        }

        if (puddleCooldownActive)
        {
            puddleCooldownImage.fillAmount += 1 / puddleCooldown * Time.deltaTime;

            if (puddleCooldownImage.fillAmount >= 1)
            {
                puddleCooldownImage.fillAmount = 0;
                puddleCooldownActive = false;
            }
        }


        // Code for Holy Water (currently mapped to 2)
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            holyWaterCooldownActive = true;
        }

        if (holyWaterCooldownActive)
        {
            holyWaterCooldownImage.fillAmount += 1 / holyWaterCooldown * Time.deltaTime;

            if (holyWaterCooldownImage.fillAmount >= 1)
            {
                holyWaterCooldownImage.fillAmount = 0;
                holyWaterCooldownActive = false;
            }
        }


        // Code for Geyser Wall (currently mapped to 3)
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            geyserWallCooldownActive = true;
        }

        if (geyserWallCooldownActive)
        {
            geyserWallCooldownImage.fillAmount += 1 / geyserWallCooldown * Time.deltaTime;

            if (geyserWallCooldownImage.fillAmount >= 1)
            {
                geyserWallCooldownImage.fillAmount = 0;
                geyserWallCooldownActive = false;
            }
        }

    }

    public void TeleportBlocked(bool isBlocked)
    {
        blocked = isBlocked;
    }



}
