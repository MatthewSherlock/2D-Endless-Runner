using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpEff : UpgradesBase
{/*
    [SerializeField]
    private int _PowerUpEffPer;

    public override void BuyUpgrade()
    {
        if (_QuantityBought < _BuyLimit)
        {
            if (PlayerPrefs.GetInt("_CoinTotal") >= _ShopCost)
            {
                PlayerPrefs.SetInt("_CoinTotal", PlayerPrefs.GetInt("_CoinTotal") - _ShopCost);
                _QuantityBought++;
                ApplyUpgrade();
            }
        }
    }

    public override void ApplyUpgrade()
    {
        DataManager._Instance._EffPUBought += _PowerUpEffPer;
    }
    */
}
