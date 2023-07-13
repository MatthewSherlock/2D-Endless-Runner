using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraArrow : MonoBehaviour
{

    private float _Distance;
    private float _DistanceScale;
    private Vector3 _InitScale;

    [SerializeField]
    private GameObject _ScaledPlayer;

    private GameObject _Player;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _Player = GameObject.FindGameObjectWithTag("Player");
        _InitScale = _ScaledPlayer.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        _Distance = Vector2.Distance(_Player.transform.position, this.transform.position);
        _DistanceScale = Mathf.Clamp(_InitScale.magnitude / (_Distance/2),0.1f,_InitScale.magnitude);
        
        
        _ScaledPlayer.transform.localScale = new Vector3(_DistanceScale, _DistanceScale, _DistanceScale);
        if (_Player.transform.position.y+0.75f <= transform.position.y)
            gameObject.SetActive(false);

    }
}
