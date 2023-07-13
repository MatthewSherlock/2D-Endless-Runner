using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairUpgrade : UpgradesBase
{
    [SerializeField]
    private int _TimeIncPerBuy;

    public override void BuyUpgrade()
    {

        if (_QuantityBought < _BuyLimit)
        {
            if (DataManager._Instance._CoinAmount >= _ShopCost)
            {
                _QuantityBought++;
                DataManager._Instance._CoinAmount -= _ShopCost;
                Debug.Log("Repair Upgrade bought");

                ApplyUpgrade();
            }
        }
    }

    public override void ApplyUpgrade()
    {
        DataManager._Instance._InvinDur += _TimeIncPerBuy;
    }
}