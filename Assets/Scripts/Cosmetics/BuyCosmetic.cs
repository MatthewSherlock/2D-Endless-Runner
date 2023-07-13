using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BuyCosmetic : MonoBehaviour
{
    [SerializeField]
    private int cosmeticNum;

    public void PurchaseButton() //public as it is used on a button
    {
        var cosmeticManager = CosmeticManager._Instance;
        if (cosmeticManager.cosmeticArray[cosmeticNum]._HasBought) //Equips if already bought 
        {
            for (int i = 0; i < cosmeticManager.cosmeticArray.Length; i++)
            {
                cosmeticManager.cosmeticArray[i]._IsEquipped = false; //sets all to unequipped 
            }
            cosmeticManager.cosmeticArray[cosmeticNum]._IsEquipped = true; //equips the correct one
            DataManager._Instance._PlayerSprite = cosmeticManager.cosmeticArray[cosmeticNum]._CosmeicSprite;
        }
        else if (DataManager._Instance._CoinAmount >= cosmeticManager.cosmeticArray[cosmeticNum]._ShopCost)
        { // Buys the cosmetic
            cosmeticManager.cosmeticArray[cosmeticNum]._HasBought = true;
            DataManager._Instance._CoinAmount -= cosmeticManager.cosmeticArray[cosmeticNum]._ShopCost;

            PurchaseButton();
        }
        var buttonScripts = GameObject.FindGameObjectWithTag("CosmeticButtons").GetComponentsInChildren<BuyCosmetic>();
        for (int i = 0; i < buttonScripts.Length; i++)
        {
            buttonScripts[i].UpdateButtons(); //Updates the text on the buttons
        }
    }

    private void UpdateButtons()
    {
        var cosmeticManager = CosmeticManager._Instance;
        if (!cosmeticManager.cosmeticArray[cosmeticNum]._HasBought)
        {
            GetComponentInChildren<TextMeshProUGUI>().text = cosmeticManager.cosmeticArray[cosmeticNum]._ShopCost.ToString();
        }
        if (cosmeticManager.cosmeticArray[cosmeticNum]._HasBought && !cosmeticManager.cosmeticArray[cosmeticNum]._IsEquipped)
        {
            GetComponentInChildren<TextMeshProUGUI>().text = "Equip";
        }
        if (cosmeticManager.cosmeticArray[cosmeticNum]._HasBought && cosmeticManager.cosmeticArray[cosmeticNum]._IsEquipped)
        {
            GetComponentInChildren<TextMeshProUGUI>().text = "Equipped";
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        UpdateButtons();
    }
    /*
    private void FixedUpdate()
    {
        var cosmeticManager = CosmeticManager._Instance;

        if (!cosmeticManager.cosmeticArray[cosmeticNum]._HasBought)
        {
            GetComponentInChildren<TextMeshProUGUI>().text = "Buy: " + cosmeticManager.cosmeticArray[cosmeticNum]._ShopCost;

        }
        if (cosmeticManager.cosmeticArray[cosmeticNum]._HasBought)
        {
            GetComponentInChildren<TextMeshProUGUI>().text = "Equip";
        }
        if (cosmeticManager.cosmeticArray[cosmeticNum]._HasBought && cosmeticManager.cosmeticArray[cosmeticNum]._IsEquipped)
        {
            GetComponentInChildren<TextMeshProUGUI>().text = "Equipped";
        }
    }
    */
}
