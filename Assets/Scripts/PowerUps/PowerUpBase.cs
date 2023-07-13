using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBase : MonoBehaviour
{
    public string pickUpName;

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name.Contains("Player"))
        {
            GameObject player = other.gameObject;
            PowerUpHolder powHolder = player.GetComponent<PowerUpHolder>();
            powHolder.PickupPowerUp(this);
        }
    }



    public virtual void UsePowerUp(GameObject player)
    {

    }

    public virtual void PickUpPowerUp(GameObject player)
    {

    }
}
