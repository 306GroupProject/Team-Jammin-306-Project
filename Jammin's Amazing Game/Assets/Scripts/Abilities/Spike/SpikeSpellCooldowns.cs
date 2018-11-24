using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class SpikeSpellCooldowns : MonoBehaviour {

    // Teleport
    public Image teleportCooldownImage; // The mask used for the radial cooldown UI
    float teleportCooldown;
    bool teleportCooldownActive;
    bool blocked = false; // Was the teleport blocked by a wall bein in the way?
    Teleport teleportScript;

    // Rock Throw
    public Image rockThrowCooldownImage;
    float rockThrowCooldown;
    bool rockThrowCooldownActive;

    // Rock Wall
    public Image rockWallCooldownImage;
    float rockWallCooldown;
    bool rockWallCooldownActive;

    // Spike Wings
    public Image spikeWingsCooldownImage;
    float spikeWingsCooldown;
    bool spikeWingsCooldownActive;

    void Start()
    {
        teleportCooldown = 3;
        rockThrowCooldown = 3;
        rockWallCooldown = 5;
        spikeWingsCooldown = 20;
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


        // Code for Rock Throw (currently mapped to 1)
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


        // Code for Rock Wall (currently mapped to 2)
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            rockWallCooldownActive = true;
        }

        if (rockWallCooldownActive)
        {
            rockWallCooldownImage.fillAmount += 1 / rockWallCooldown * Time.deltaTime;

            if (rockWallCooldownImage.fillAmount >= 1)
            {
                rockWallCooldownImage.fillAmount = 0;
                rockWallCooldownActive = false;
            }
        }


        // Code for Spike Wings (currently mapped to 3)
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            spikeWingsCooldownActive = true;
        }

        if (spikeWingsCooldownActive)
        {
            spikeWingsCooldownImage.fillAmount += 1 / spikeWingsCooldown * Time.deltaTime;

            if (spikeWingsCooldownImage.fillAmount >= 1)
            {
                spikeWingsCooldownImage.fillAmount = 0;
                spikeWingsCooldownActive = false;
            }
        }

    }

    public void TeleportBlocked(bool isBlocked)
    {
        blocked = isBlocked;
    }

    

}
