using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    private Score _Score;
    public void EndGame()
    {
        Debug.Log("end");
        _Score.OnGameEnd();
    }

    // Start is called before the first frame update
    void Start()
    {
        _Score = GetComponent<Score>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
