using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeGroundObjectSpawning : MonoBehaviour
{
    [SerializeField]
    private float _CoinSpawnTimer, _CoinTimerVariation, _ObstacleSpawnTimer, _ObstacleTimerVariation, _PowerUpSpawnTimer, _PowerUpTimerVariation;
    [SerializeField]
    private float _SkyCoinSpawnTimer, _SkyCoinTimerVariation, _SkyObstacleSpawnTimer, _SkyObstacleTimerVariation, _SkyPowerUpSpawnTimer, _SkyPowerUpTimerVariation, _YOffset;
    
    [SerializeField]
    private int _MaxCoins;
    [SerializeField]
    private float _MinHeight, _MaxHeight;
    [SerializeField]
    GameObject[] _GroundObstacles,_SkyObstacles,_Coins,_PowerUps;
    Vector3 _Hitpoint;
    GameObject _HitObject;
    [SerializeField]
    ContactFilter2D Filter;
    GameObject MainWorld;
    
    private void UpdateRay()
    {
        //DebugCube.transform.position = new Vector3(transform.position.x + Camera.main.orthographicSize * 3,transform.position.y + Camera.main.orthographicSize * 2,0);
        Vector3 _RayPosition = new Vector3(transform.position.x + Camera.main.orthographicSize * 3,transform.position.y +Camera.main.orthographicSize * 2,0);
        RaycastHit2D _Hit = Physics2D.Raycast(_RayPosition, Vector2.down,Filter.layerMask,1, Mathf.Infinity);
        if (_Hit.transform != null)
        {
            _Hitpoint = new Vector3(_Hit.point.x, _Hit.point.y, 0);
            _HitObject = _Hit.transform.gameObject;
        }
        else _HitObject = null;
    }

    private IEnumerator SpawnCoinGround()
    {
        
        float _Value = Random.Range(_CoinSpawnTimer - _CoinTimerVariation, _CoinSpawnTimer + _CoinTimerVariation);
        
        yield return new WaitForSeconds(_Value);
        UpdateRay();
        
        if (_HitObject != null && GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerFreeMovement>().Enabled)
            SpawnCoins();
        StartCoroutine(SpawnCoinGround());

    }

    private IEnumerator SpawnObstacleGround()
    {
        float _Value = Random.Range(_ObstacleSpawnTimer - _ObstacleTimerVariation, _ObstacleSpawnTimer + _ObstacleTimerVariation);

        yield return new WaitForSeconds(_Value);
        UpdateRay();
        
        if (_HitObject != null && GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerFreeMovement>().Enabled)
            SpawnObstacles();
        StartCoroutine(SpawnObstacleGround());
    }
    private IEnumerator SpawnPowerUp()
    {

        float _Value = Random.Range(_PowerUpSpawnTimer - _PowerUpTimerVariation, _PowerUpSpawnTimer + _PowerUpTimerVariation);

        yield return new WaitForSeconds(_Value);
        UpdateRay();
        if (_HitObject != null && GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerFreeMovement>().Enabled)
        {
            SpawnPowerUps();
        }
        StartCoroutine(SpawnPowerUp());

    }

    private void SpawnCoins()
    {
        int CoinsNum = Random.Range(0, _MaxCoins);

        StartCoroutine(CoinCoroutine(CoinsNum));
    }

    private void SpawnPowerUps()
    {
        Vector3 Tempscale = _PowerUps[0].transform.localScale;
        GameObject SpawnObject = Instantiate(_PowerUps[Random.Range(0, _PowerUps.Length)], new Vector3(_Hitpoint.x, _Hitpoint.y + _YOffset), transform.rotation, MainWorld.transform);
        
    }


    private IEnumerator CoinCoroutine(int Value)
    {
        yield return new WaitForSeconds(0.5f);
        UpdateRay();
        if (_HitObject != null)
        {
            Vector3 Tempscale = _Coins[0].transform.localScale;
            GameObject SpawnObject = Instantiate(_Coins[Random.Range(0, _Coins.Length)], new Vector3(_Hitpoint.x, _Hitpoint.y + _YOffset), transform.rotation, MainWorld.transform);
            
            Value--;
        }
        if (Value > 0)
            StartCoroutine(CoinCoroutine(Value));
    }


    private void SpawnObstacles()
    {
        Vector3 Tempscale = _GroundObstacles[0].transform.localScale;
        GameObject SpawnObject = Instantiate(_GroundObstacles[Random.Range(0, _GroundObstacles.Length)], _Hitpoint, transform.rotation, MainWorld.transform);
        
    }


    private IEnumerator SpawnSkyObstaclesTimer()
    {
        float _Value = Random.Range(_SkyObstacleSpawnTimer - _SkyObstacleTimerVariation, _SkyObstacleSpawnTimer + _SkyObstacleTimerVariation);

        yield return new WaitForSeconds(_Value);
        UpdateRay();

        if (_HitObject != null && GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerFreeMovement>().Enabled)
            SpawnSkyObstacles();
        
        StartCoroutine(SpawnSkyObstaclesTimer());

    }
    private IEnumerator SpawnSkyCoinTimer()
    {
        float _Value = Random.Range(_SkyCoinSpawnTimer - _SkyCoinTimerVariation, _SkyCoinSpawnTimer + _SkyCoinTimerVariation);

        yield return new WaitForSeconds(_Value);
        UpdateRay();

        if (_HitObject != null && GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerFreeMovement>().Enabled)
            SpawnSkyCoins();
       
        StartCoroutine(SpawnSkyCoinTimer());
    }

    private IEnumerator SpawnSkyPowerUpsTimer()
    {
        float _Value = Random.Range(_SkyPowerUpSpawnTimer - _SkyPowerUpTimerVariation, _SkyPowerUpSpawnTimer + _SkyPowerUpTimerVariation);

        yield return new WaitForSeconds(_Value);
        UpdateRay();

        if (_HitObject != null && GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerFreeMovement>().Enabled)
            SpawnSkyPowerUps();
        
        StartCoroutine(SpawnSkyPowerUpsTimer());
    }


    private void SpawnSkyObstacles()
    {
        float height = Random.Range(_MinHeight, _MaxHeight);
        Vector3 Tempscale = _SkyObstacles[0].transform.localScale;
        GameObject SpawnObject = Instantiate(_SkyObstacles[Random.Range(0, _SkyObstacles.Length)],new Vector3(_Hitpoint.x,_Hitpoint.y + height,0), transform.rotation, MainWorld.transform);
        
        
    }
    private void SpawnSkyCoins()
    {
        int CoinsNum = Random.Range(0, _MaxCoins);
        float Height = Random.Range(_MinHeight,_MaxHeight);
        StartCoroutine(SkyCoinCoroutine(CoinsNum,Height));
        
    }

    private IEnumerator SkyCoinCoroutine(int Value,float Height)
    {
        yield return new WaitForSeconds(0.5f);
        UpdateRay();
        if (_HitObject != null)
        {
            Vector3 Tempscale = _Coins[0].transform.localScale;
            GameObject SpawnObject = Instantiate(_Coins[Random.Range(0, _Coins.Length)], new Vector3(_Hitpoint.x, _Hitpoint.y + Height), transform.rotation, MainWorld.transform);
            
            Value--;
        }
        if (Value > 0)
            StartCoroutine(SkyCoinCoroutine(Value,Height));
    }

    private void SpawnSkyPowerUps()
    {
        float height = Random.Range(_MinHeight, _MaxHeight);
        Vector3 Tempscale = _PowerUps[0].transform.localScale;
        GameObject SpawnObject = Instantiate(_PowerUps[Random.Range(0, _PowerUps.Length)], new Vector3(_Hitpoint.x, _Hitpoint.y + height, 0), transform.rotation, MainWorld.transform);
        
    }

    private void Start()
    {
        MainWorld = GameObject.FindGameObjectWithTag("MainWorld");
        // Ground Spawner
        StartCoroutine(SpawnObstacleGround());
        StartCoroutine(SpawnCoinGround());
        StartCoroutine(SpawnPowerUp());

        // Sky Spawner
        StartCoroutine(SpawnSkyObstaclesTimer());
        StartCoroutine(SpawnSkyCoinTimer());
        StartCoroutine(SpawnSkyPowerUpsTimer());
    }

}
