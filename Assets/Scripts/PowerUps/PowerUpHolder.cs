using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PowerUpHolder : MonoBehaviour
{

    public PowerUpBase currPowerUp;
    private GameObject player;
    [SerializeField]
    private TextMeshProUGUI displayText;
    private bool hasSpeedBoost, hasRepair;

    private void OnTriggerEnter2D(Collider2D other)
    {
        /*
        //        Debug.Log("pow");
        if (other.gameObject.GetComponent<PowerUpBase>())
        {

            hasSpeedBoost = true;
            Destroy(other.gameObject);
            Debug.Log("speed boost TRUE");

        }
        if (other.gameObject.name.Contains("RepairPowerUp"))
        {
            hasRepair = true;
            Destroy(other.gameObject);
        }
        */
    }

    private void Start()
    {
        player = gameObject;
    }
    /*
    public void UseSpeedPowerUp()
    {
        if (hasSpeedBoost)
        {
            Debug.Log("speed boost USED");

            hasSpeedBoost = false;
            GetComponent<SpeedPowerUp>().ActivatePowerUp(player);
        }
        else
        {
            Debug.Log("NO speed boost");

        }
    }
    public void UseRepairPowerUp()
    {
        if (hasRepair)
        {
            Debug.Log("Repair USED");
            hasRepair = false;
            GetComponent<RepairPowerUp>().ActivatePowerUp(player);
        }
        else
        {
            Debug.Log("NO repair");

        }
    }
    */

    public void ActivatePowerUp()
    {
        if (currPowerUp)
        {
            currPowerUp.UsePowerUp(player);
            displayText.text = ("No Power Up ");
            currPowerUp.GetComponent<AudioSource>().Play();
            StartCoroutine(Deletion(currPowerUp.GetComponent<AudioSource>().clip.length, currPowerUp.gameObject));
            currPowerUp = null;
        }
    }

    private IEnumerator Deletion(float Length,GameObject Powerup)
    {
        yield return new WaitForSeconds(Length);
        Destroy(Powerup);
        
    }
     public void PickupPowerUp(PowerUpBase pow)
    {
        if(!currPowerUp)
        {
            //Debug.Log("pickup");
            currPowerUp = pow;
            DisablePowerUp(pow);
            displayText.text = ("Use "  + currPowerUp.pickUpName);

        }
        else
        {
           // Debug.Log("NO pickup");

        }
    }
    private void DisablePowerUp(PowerUpBase pow)
    {
        pow.GetComponent<SpriteRenderer>().enabled = false;
        pow.GetComponent<CircleCollider2D>().enabled = false;
    }
}
