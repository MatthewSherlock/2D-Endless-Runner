using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingBoost : UpgradesBase
{
    [SerializeField]
    public float BoostAmount;
    public override void ApplyUpgrade()
    {
        DataManager._Instance._StartingBoostBought++;
    }
    public void UseSpeedBoost()
    {
        GameObject player = GameObject.Find("Player");
        Rigidbody2D _rb = player.GetComponent<Rigidbody2D>();

        _rb.AddForce(Vector2.right * BoostAmount, ForceMode2D.Impulse);
        Debug.Log("speed boost ACTIVATE");
    }
    private void Start()
    {
    }
}
