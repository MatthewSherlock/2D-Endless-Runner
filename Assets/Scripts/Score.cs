using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Score : MonoBehaviour
{
    [SerializeField]
    private float currScore;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private TextMeshProUGUI scoreDisplayText, coinDisplayText, pauseCoinDisplayText;


    public float GetScore()
    {
        return currScore;
    }
    public void OnGameEnd()
    {
        DataManager._Instance._TotalScore = Mathf.RoundToInt(currScore);
        if(DataManager._Instance._TotalScore > DataManager._Instance._Highscore)
        {
            DataManager._Instance._Highscore = DataManager._Instance._TotalScore;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currScore = Mathf.RoundToInt(player.transform.localPosition.x - 4);
        scoreDisplayText.text = (currScore.ToString() + "M");

        coinDisplayText.text = (DataManager._Instance._CoinAmount.ToString());
      //  pauseCoinDisplayText.text = (DataManager._Instance._CoinAmount.ToString());

    }
}
