using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairPowerUp : PowerUpBase
{
    public override void UsePowerUp(GameObject player)
    {
        Debug.Log("repair ACTIVATE");
        player.GetComponent<PlayerFreeMovement>().isInvincible = true;

        player.GetComponent<PlayerFreeMovement>().StartCoroutine("Invincible"); //StartCoroutine(Invincible(duration));
        
    }

}
