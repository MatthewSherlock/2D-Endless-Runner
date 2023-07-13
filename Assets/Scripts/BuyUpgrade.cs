using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BuyUpgrade : MonoBehaviour
{
    
    public enum Upgrades {SpeedBoost, AirTimeBoost, Repair, StartingBoost, StartingbBoostRange};
    [SerializeField]
    private Upgrades upgradeSelected;
    private UpgradesBase upgrade;


    void GetUpgradeSelected()
    {
        switch (upgradeSelected)
        {
            case Upgrades.SpeedBoost:
                upgrade = UpgradeManager._Instance.GetComponent<SpeedBoostUpgrade>();
                break;
            case Upgrades.AirTimeBoost:
                upgrade = UpgradeManager._Instance.GetComponent<AirTimeUpgrade>();
                break;
            case Upgrades.Repair:
                upgrade = UpgradeManager._Instance.GetComponent<RepairUpgrade>();
                break;
            case Upgrades.StartingBoost:
                upgrade = UpgradeManager._Instance.GetComponent<StartingBoost>();
                break;
            case Upgrades.StartingbBoostRange:
                upgrade = UpgradeManager._Instance.GetComponent<StartingBoostRange>();
                break;
            default:
                break;
        }
    }

    public void PurchaseButton()
    {
        GetUpgradeSelected();
        Debug.Log("Upgrade bought" + upgrade);
        if (upgrade._QuantityBought < upgrade._BuyLimit)
        {
            if (DataManager._Instance._CoinAmount >= upgrade._ShopCost)
            {
                upgrade._QuantityBought++;
                DataManager._Instance._CoinAmount -= upgrade._ShopCost;
                upgrade._ShopCost += 10;
                upgrade.ApplyUpgrade();
                UpdatePrices();
            }
        }

    }

    void UpdatePrices()
    {
        GetUpgradeSelected();

        if (upgrade._QuantityBought == upgrade._BuyLimit)
        {
            GetComponentInChildren<TextMeshProUGUI>().text = "Max Bought!";
        }
        else
        {
            GetComponentInChildren<TextMeshProUGUI>().text = upgrade._ShopCost.ToString();
        }


    }

    // Start is called before the first frame update
    void Start()
    {
        UpdatePrices();
    }

}
