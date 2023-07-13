using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CloudConstallation : MonoBehaviour
{

    SpriteRenderer _Sprite;

    private bool _Day = false;
    [SerializeField]
    Sprite[] Sprites;



   
    
    // Start is called before the first frame update
    void Start()
    {
        
        _Sprite = GetComponent<SpriteRenderer>();
        _Day = GameObject.FindGameObjectWithTag("DayNight").GetComponent<DayNightCycle>().isSunInScene;
        if (_Day)
            _Sprite.sprite = Sprites[1];
        else
            _Sprite.sprite = Sprites[0];
        Debug.Log(_Day);
    }
    

}
