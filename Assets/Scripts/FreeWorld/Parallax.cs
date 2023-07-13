using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField]
    private float _ParallaxEffect;
    [SerializeField]
    private bool _MoveRight;
    private Transform _CameraTransform;
    private Vector3 _LastCameraPosition;
    private float _Length, _StartPos;


    // Start is called before the first frame update
    void Start()
    {
        _CameraTransform = Camera.main.transform;
        _LastCameraPosition = _CameraTransform.localPosition;
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        _Length = (texture.width / sprite.pixelsPerUnit) * transform.localScale.x;
        _StartPos = transform.parent.transform.position.x;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        
        Vector3 deltaMovement = _CameraTransform.localPosition - _LastCameraPosition;


        transform.parent.transform.position += new Vector3(deltaMovement.x * _ParallaxEffect, 0);
        _LastCameraPosition = _CameraTransform.localPosition;
        //if (_CameraTransform.position.x - transform.parent.transform.position.x >= _Length)
        //{
        //    float OffsetPositionX = (_LastCameraPosition.x - transform.parent.transform.position.x) % _Length;
        //    transform.parent.transform.position = new Vector3(_LastCameraPosition.x + OffsetPositionX, transform.parent.transform.position.y);
        //}

        
       
        
        
        /*
        float Temp = (_CameraTransform.position.x * (1 - _ParallaxEffect));
        float Dist = (_LastCameraPosition.x * _ParallaxEffect);

        transform.position = new Vector3(_StartPos + Dist, transform.position.y, 0);

        if (Temp > _StartPos + _Length) _StartPos += _Length;
        else if (Temp < _StartPos - _Length) _StartPos -= _Length;
        */
        /*
        if(_CameraTransform.position.x - transform.position.x >= _Length)
        {
            float offsetPositionX = (_CameraTransform.position.x - transform.position.x) % _Length;
            transform.position = new Vector3(_CameraTransform.position.x + offsetPositionX, transform.position.y);

        }
        */
        
    }
}
