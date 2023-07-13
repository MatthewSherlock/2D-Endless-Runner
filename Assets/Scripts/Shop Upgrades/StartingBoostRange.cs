using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingBoostRange : UpgradesBase
{
    [SerializeField]
    private int StartingBoostRangeIncrease;
    public override void ApplyUpgrade()
    {
        UpgradeManager._Instance.GetComponent<StartingBoost>().BoostAmount += StartingBoostRangeIncrease;
    }
}
