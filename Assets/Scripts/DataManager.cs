using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour
{

    public static DataManager _Instance;
    public int _SpeedUpgradesBought; //, _AirUpgradesBought, _EffPUBought; //PU refers to Power Up
    public int _CoinAmount, _SpeedBoostAmount;
    public int _TotalScore, _Highscore, _CurrScore;
 // public float _SpeedModifierPer, _AirModifierPer;
    public int _SpriteNum, _InvinDur = 1, _StartingBoostBought;
    [SerializeField]
    private TextMeshProUGUI _ShopCoinDisplayText, _CosmeticCoinDisplayText;
    public Sprite _PlayerSprite;
    int isBought, isEquipped, hadBought, hadEquipped;

    private void Awake()
    {
        if (_Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        LoadGame();
        Application.targetFrameRate = 60;
        

        _Instance = this;
        DontDestroyOnLoad(gameObject);

    }

    void LoadGame()
    {
        _CoinAmount = PlayerPrefs.GetInt("CoinAmount");
       // _SpeedModifierPer = PlayerPrefs.GetFloat("SpeedModifierPercentage");
      //  _AirModifierPer =  PlayerPrefs.GetFloat("AirModifierPercentage");
        _SpeedUpgradesBought =  PlayerPrefs.GetInt("SpeedUpgradesBought");
        //_AirUpgradesBought =  PlayerPrefs.GetInt("AirUpgradesBought");
        //_PUQuantityBought = PlayerPrefs.GetInt("PowerUpQuantityBought");
       // _RepairNumBought = PlayerPrefs.GetInt("RepairUpgradesBought");
        _Highscore =  PlayerPrefs.GetInt("HighScore");
        _SpriteNum = PlayerPrefs.GetInt("EquippedSprite");
        _InvinDur = PlayerPrefs.GetInt("InvincibleDuration");
        _SpeedBoostAmount = PlayerPrefs.GetInt("BoostAmount");
        _StartingBoostBought = PlayerPrefs.GetInt("StartingBoostAmount");

        UpgradeManager._Instance.GetComponent<SpeedBoostUpgrade>()._QuantityBought = PlayerPrefs.GetInt("SpeedUpgradesBought"); ;
        UpgradeManager._Instance.GetComponent<RepairUpgrade>()._QuantityBought = PlayerPrefs.GetInt("RepairUpgradesBought");

        if (_InvinDur <= 0)
        {
            _InvinDur = 1;
        }
        if(_SpeedBoostAmount < 15)
        {
            _SpeedBoostAmount = 15;
        }

    }
    private void Start()
    {
        LoadCosmetics();

        // LoadGame();
        //Application.targetFrameRate = 60;
    }

    public void ResetData()
    {
        PlayerPrefs.SetInt("CoinAmount", 0);
        PlayerPrefs.SetFloat("SpeedModifierPercentage", 0);
        PlayerPrefs.SetFloat("AirModifierPercentage", 0);
        PlayerPrefs.SetInt("SpeedUpgradesBought", 0);
        PlayerPrefs.SetInt("AirUpgradesBought", 0);
        PlayerPrefs.SetInt("PowerUpQuantityBought", 0);
        PlayerPrefs.SetInt("RepairUpgradesBought", 0);
        PlayerPrefs.SetInt("HighScore", 0);
        PlayerPrefs.SetInt("EquippedSprite", 0);
        PlayerPrefs.SetInt("InvincibleDuration", 1);
        PlayerPrefs.SetInt("BoostAmount", 15);
        PlayerPrefs.SetInt("StartingBoostAmount", 0);
        ResetCosmetics();
        LoadGame();
    }
    private void SaveGame()
    {
        PlayerPrefs.SetInt("CoinAmount", _CoinAmount);
        //PlayerPrefs.SetFloat("SpeedModifierPercentage", _SpeedModifierPer);
        //PlayerPrefs.SetFloat("AirModifierPercentage", _AirModifierPer);
        PlayerPrefs.SetInt("SpeedUpgradesBought", UpgradeManager._Instance.GetComponent<SpeedBoostUpgrade>()._QuantityBought);
        //PlayerPrefs.SetInt("AirUpgradesBought", _AirUpgradesBought);
        //PlayerPrefs.SetInt("PowerUpQuantityBought", _PUQuantityBought);
        PlayerPrefs.SetInt("RepairUpgradesBought", UpgradeManager._Instance.GetComponent<RepairUpgrade>()._QuantityBought);
        PlayerPrefs.SetInt("HighScore", _Highscore);
        PlayerPrefs.SetInt("EquippedSprite", _SpriteNum);
        PlayerPrefs.SetInt("InvincibleDuration", _InvinDur);
        PlayerPrefs.SetInt("BoostAmount", _SpeedBoostAmount);
        PlayerPrefs.SetInt("StartingBoostAmount", _StartingBoostBought);
        SaveCosmetics();
        PlayerPrefs.Save();

    }

    private void ResetCosmetics()
    {
        var cosmeticManager = CosmeticManager._Instance;
        for (int i = 0; i < cosmeticManager.cosmeticArray.Length; i++)
        {
            if(i != 0)
            cosmeticManager.cosmeticArray[i]._HasBought = false;
        }
        cosmeticManager.cosmeticArray[0]._IsEquipped = true;
        _PlayerSprite = cosmeticManager.cosmeticArray[0]._CosmeicSprite;
    }

    private void SaveCosmetics()
    {
        var cosmeticManager = CosmeticManager._Instance;
        for (int i = 0; i < cosmeticManager.cosmeticArray.Length; i++)
        {
            if (cosmeticManager.cosmeticArray[i]._HasBought)
            {
               isBought = 1;
            }
            if (!cosmeticManager.cosmeticArray[i]._HasBought)
            {
               isBought = 0;
            }
            if (cosmeticManager.cosmeticArray[i]._IsEquipped)
            {
                isEquipped = 1;
            }
            if (!cosmeticManager.cosmeticArray[i]._IsEquipped)
            {
                isEquipped = 0;
            }


            PlayerPrefs.SetInt(i.ToString(), isBought);
            PlayerPrefs.SetInt((i+100).ToString(), isEquipped);


            PlayerPrefs.Save();
        }
    }

    private void LoadCosmetics()
    {
        bool temp = false;
        var cosmeticManager = CosmeticManager._Instance;
        for (int i = 0; i < cosmeticManager.cosmeticArray.Length; i++)
        {
            hadBought = PlayerPrefs.GetInt(i.ToString());
            if(hadBought == 1)
            {
                cosmeticManager.cosmeticArray[i]._HasBought = true;
            }
            if (hadBought == 0)
            {
                cosmeticManager.cosmeticArray[i]._HasBought = false;
            }

            hadEquipped = PlayerPrefs.GetInt((i+100).ToString());
            if (hadEquipped == 1)
            {
                cosmeticManager.cosmeticArray[i]._IsEquipped = true;
                _PlayerSprite = cosmeticManager.cosmeticArray[i]._CosmeicSprite;
                temp = true;
            }
            if (hadEquipped == 0)
            {
                cosmeticManager.cosmeticArray[i]._IsEquipped = false;

            }
        }
        if (temp == false)
        {
            cosmeticManager.cosmeticArray[0]._HasBought = true;
            cosmeticManager.cosmeticArray[0]._IsEquipped = true;
        }
    }
    private void OnApplicationQuit()
    {
        SaveGame();
    }
    private void OnApplicationFocus(bool focus)
    {
        
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(0))
        {
            if (!_ShopCoinDisplayText)
            {
                _ShopCoinDisplayText = GameObject.Find("CoinsDisplay").GetComponent<TextMeshProUGUI>();
            }
            else if (_ShopCoinDisplayText != null)
                _ShopCoinDisplayText.text = _CoinAmount.ToString();

            if (!_CosmeticCoinDisplayText)
            {
                _CosmeticCoinDisplayText = GameObject.Find("CoinDisplay").GetComponent<TextMeshProUGUI>();
            }
            else if (_CosmeticCoinDisplayText != null)
                _CosmeticCoinDisplayText.text = _CoinAmount.ToString();

        }
    }
}
