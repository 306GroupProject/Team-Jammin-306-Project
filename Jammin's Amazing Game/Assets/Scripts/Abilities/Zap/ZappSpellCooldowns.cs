using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class ZappSpellCooldowns : MonoBehaviour
{
    // Teleport
    public Image teleportCooldownImage; // The mask used for the radial cooldown UI
    float teleportCooldown;
    bool teleportCooldownActive;
    bool blocked = false; // Was the teleport blocked by a wall bein in the way?
    Teleport teleportScript;

    // Lightning Bolt
    public Image lightningBoltCooldownImage;
    float lightningBoltCooldown;
    bool lightningBoltCooldownActive;

    // Super Charge
    public Image superChargeCooldownImage;
    float superChargeCooldown;
    bool superChargeCooldownActive;

    // Volt Tackle
    public Image voltTackleCooldownImage;
    float voltTackleCooldown;
    bool voltTackleCooldownActive;

    void Start()
    {
        teleportCooldown = 3;
        lightningBoltCooldown = 3;
        superChargeCooldown = 15;
        voltTackleCooldown = 5;
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


        // Code for Lightning Bolt (currently mapped to 1)
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


        // Code for Super Charge (currently mapped to 2)
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            superChargeCooldownActive = true;
        }

        if (superChargeCooldownActive)
        {
            superChargeCooldownImage.fillAmount += 1 / superChargeCooldown * Time.deltaTime;

            if (superChargeCooldownImage.fillAmount >= 1)
            {
                superChargeCooldownImage.fillAmount = 0;
                superChargeCooldownActive = false;
            }
        }

        // Code for Volt Tackle (currently mapped to 3)
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            voltTackleCooldownActive = true;
        }

        if (voltTackleCooldownActive)
        {
            voltTackleCooldownImage.fillAmount += 1 / voltTackleCooldown * Time.deltaTime;

            if (voltTackleCooldownImage.fillAmount >= 1)
            {
                voltTackleCooldownImage.fillAmount = 0;
                voltTackleCooldownActive = false;
            }
        }

    }

    public void TeleportBlocked(bool isBlocked)
    {
        blocked = isBlocked;
    }



}
