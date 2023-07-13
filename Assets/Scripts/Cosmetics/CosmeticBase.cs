using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CosmeticBase : MonoBehaviour
{
    [SerializeField]
    private int _ShopCost;
    private bool _HasBought, isEquipped;
    [SerializeField]
    private Sprite cosmetic;
    /*
    public void BuyCosmetic()
    {
        if(DataManager._Instance._CoinAmount >= _ShopCost && _HasBought == false)
        {
            _HasBought = true;
            DataManager._Instance._CoinAmount -= _ShopCost;
            Debug.Log("Bought");
        }
        if (_HasBought == true)
            EquipCosmetic();
    }

    public void EquipCosmetic()
    {
        Debug.Log("Equipped");
        DataManager._Instance._PlayerSprite = cosmetic;
    }
    */
}
