using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SpellCooldowns : MonoBehaviour {

    public Image cooldownImage;
    public float cooldown = 3; // set to 3 seconds for the teleport at the moment
    bool cooldownActive;
    bool blocked = false;

    // Update is called once per frame
    void Update () {

        if (!blocked)
        {
            if (Input.GetMouseButtonDown(1)) // if the player right clicks to teleport, activate the cooldown UI
            {
                cooldownActive = true;
            }

            if (cooldownActive)
            {
                cooldownImage.fillAmount += 1 / cooldown * Time.deltaTime;

                if (cooldownImage.fillAmount >= 1) // after the radial animation plays, set everything back to its default state
                {
                    cooldownImage.fillAmount = 0;
                    cooldownActive = false;
                }
            }
        }
	}

    public void TeleportBlocked(bool isBlocked)
    {
        blocked = isBlocked;
    }
}
