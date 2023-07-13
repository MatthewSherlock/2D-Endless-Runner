using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShipChasser : MonoBehaviour
{
    [SerializeField]
    private int CoinGainForDistance;
    private float _ShipSpeed;
    [SerializeField]
    private float _TimeTillChase = 3f;
    [SerializeField]
    GameObject resultsScreen;

    private float _DistanceIncrease;
    [SerializeField]
    private float _MinSpeed = 5f;
    private bool _Chasing = false;
    private Score _Score;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            _Score.OnGameEnd();
            DistancetoCoins();
            collision.GetComponent<PlayerFreeMovement>().GetCaught();
            StartCoroutine(GameOver());
        }
    }

    private void DistancetoCoins()
    {
        float Score = GetComponent<Score>().GetScore();
        if(Score >= 100)
        {
            DataManager._Instance._CoinAmount += CoinGainForDistance;
            Score -= 100;
            if(Score >= 100)
            {
                while(Score >= 100)
                {
                    DataManager._Instance._CoinAmount += CoinGainForDistance;
                    Score -= 100;
                }
            }
        }
    }

    private IEnumerator GameOver()
    {

        // any Other functionallity here

        yield return new WaitForSeconds(1.5f);
        resultsScreen.SetActive(true);
    }

    private void Chase()
    {
        _ShipSpeed = (GameObject.FindGameObjectWithTag("Player").transform.position.x - transform.position.x)/2;
        _ShipSpeed = Mathf.Clamp(_ShipSpeed, _MinSpeed, Mathf.Infinity);
        transform.position += new Vector3(_ShipSpeed * Time.deltaTime, 0, 0);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -Mathf.Infinity, GameObject.FindGameObjectWithTag("Player").transform.position.x),0,0);
    }

    private IEnumerator EnableChse()
    {
        yield return new WaitForSeconds(_TimeTillChase);
        _Chasing = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        _Score = GetComponent<Score>();
        StartCoroutine(EnableChse());
    }

    // Update is called once per frame
    void Update()
    {
        if (_Chasing && GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerFreeMovement>().Enabled) Chase();
    }
}
