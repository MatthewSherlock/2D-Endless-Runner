using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ResultsScreen : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI resultsScoreDisplayText, resultsHighScore, resultsCoinDisplayText;

    void UpdateData()
    {

    }
    private void Update()
    {
        resultsCoinDisplayText.text = DataManager._Instance._CoinAmount.ToString();
        resultsHighScore.text = DataManager._Instance._Highscore.ToString();
        resultsScoreDisplayText.text = DataManager._Instance._TotalScore.ToString();
    }

}
