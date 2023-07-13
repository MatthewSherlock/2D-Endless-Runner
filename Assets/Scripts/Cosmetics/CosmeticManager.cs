using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CosmeticManager : MonoBehaviour
{
    public static CosmeticManager _Instance;


    private void Awake()
    {
        if (_Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    [SerializeField]
    public Cosmetics[] cosmeticArray;

}
[System.Serializable]
public struct Cosmetics
{
    public string name;
    public Sprite _CosmeicSprite;
    public int _ShopCost;
    public bool _HasBought;
    public bool _IsEquipped;

}


