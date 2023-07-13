using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SpeedBoostUpgrade : UpgradesBase
{
    [SerializeField]
    private int _SpeedNumIncreasePerBuy;


    public override void ApplyUpgrade()
    {
        DataManager._Instance._SpeedBoostAmount += _SpeedNumIncreasePerBuy;
    }
    private void Start()
    {
        _QuantityBought = DataManager._Instance._SpeedUpgradesBought;
    }
}
