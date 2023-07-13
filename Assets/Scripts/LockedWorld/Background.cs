using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    //erializeField,Range(0,15)]
    //private float _Speed = 1f;

    //List<Transform> BackgroundPieces;
    [SerializeField]
    GameObject[] BackgroundPrefabs;



    // Start is called before the first frame update
    void Start()
    {
        //BackgroundPieces = new List<Transform>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
