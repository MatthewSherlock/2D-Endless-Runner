using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Coin : MonoBehaviour
{
    [SerializeField]
    private int coinValue;
    [SerializeField]
    private TextMeshProUGUI coinDisplayText;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name.Contains("Player"))
        {
            DataManager._Instance._CoinAmount += coinValue;
            GetComponent<CircleCollider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            StartCoroutine(Destroyaftertime(GetComponent<AudioSource>().clip.length));
        };
    }

    IEnumerator Destroyaftertime(float Length)
    {
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(Length);
        Destroy(gameObject);
    }
    private void OnCollisionEnter(Collision other)
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
}
