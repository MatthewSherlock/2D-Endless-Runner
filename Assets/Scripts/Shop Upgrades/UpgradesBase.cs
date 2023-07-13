using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UpgradesBase : MonoBehaviour
{
    public int _ShopCost;
    public int _BuyLimit;
    public int _QuantityBought;

    public virtual void BuyUpgrade()
    {
    }

    public virtual void ApplyUpgrade()
    {
    }
}
