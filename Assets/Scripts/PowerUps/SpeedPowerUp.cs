using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerUp : PowerUpBase
{
    [SerializeField]
    private float boostAmount;

    /*
    private void OnTriggerEnter2D(Collider2D other)
    {
        //        Debug.Log("pow");
        if (other.gameObject.name.Contains("Player"))
        {
            GameObject player = other.gameObject;
            PowerUpHolder powHolder = player.GetComponent<PowerUpHolder>();
            powHolder.PickupPowerUp(this);
            Destroy(gameObject);
        }
    }
    */
    public override void UsePowerUp(GameObject player)
    {
        Rigidbody2D _rb = player.GetComponent<Rigidbody2D>();

        _rb.AddForce(Vector2.right * DataManager._Instance._SpeedBoostAmount, ForceMode2D.Impulse);
        Debug.Log("speed boost ACTIVATE");


    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
