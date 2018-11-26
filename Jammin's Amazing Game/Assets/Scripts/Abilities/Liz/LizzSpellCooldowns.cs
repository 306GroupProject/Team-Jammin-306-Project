using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

// The Cooldown UI for all of Liz's spells and abilities

public class LizzSpellCooldowns : NetworkBehaviour
{
    // Teleport
    public Image teleportCooldownImage; // The mask used for the radial cooldown UI
    float teleportCooldown;
    bool teleportCooldownActive;
    bool blocked = false; // Was the teleport blocked by a wall bein in the way? (NOT USED, IN PROGRESS!)
    Teleport teleportScript;

    // Fire Ball
    public Image fireBallCooldownImage;
    float fireBallCooldown;
    // FireBall fireBallScript;
    bool fireBallCooldownActive;

    // Flamethrower
    public Image flamethrowerCooldownImage;
    float flamethrowerCooldown;
    bool flamethrowerCooldownActive;

    // Explosion
    public Image explosionCooldownImage;
    float explosionCooldown;
    bool explosionCooldownActive;

    GameObject finnCooldownCanvas;

    void Start()
    {
        finnCooldownCanvas = GameObject.FindGameObjectWithTag("FinnCdCanvas");
        teleportCooldown = 3;
        fireBallCooldown = 2;
        flamethrowerCooldown = 10;
        explosionCooldown = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer)
        {
            finnCooldownCanvas.SetActive(false);
        }

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

        // Code for Fire Ball (currently mapped to 1)
        if (Input.GetKeyDown(KeyCode.Alpha1))
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


        // Code for Flamethrower (currently mapped to 2)
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            flamethrowerCooldownActive = true;
        }

        if (flamethrowerCooldownActive)
        {
            flamethrowerCooldownImage.fillAmount += 1 / flamethrowerCooldown * Time.deltaTime;

            if (flamethrowerCooldownImage.fillAmount >= 1)
            {
                flamethrowerCooldownImage.fillAmount = 0;
                flamethrowerCooldownActive = false;
            }
        }


        // Code for Explosion (currently mapped to 3)
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            explosionCooldownActive = true;
        }

        if (explosionCooldownActive)
        {
            explosionCooldownImage.fillAmount += 1 / explosionCooldown * Time.deltaTime;

            if (explosionCooldownImage.fillAmount >= 1)
            {
                explosionCooldownImage.fillAmount = 0;
                explosionCooldownActive = false;
            }
        }

    }

    public void TeleportBlocked(bool isBlocked)
    {
        blocked = isBlocked;
    }



}
