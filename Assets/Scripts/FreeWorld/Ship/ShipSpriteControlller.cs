using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSpriteControlller : MonoBehaviour
{

    Vector3 _Hitpoint;
    GameObject ShipCollider;
    [SerializeField]
    private float MoveTime = 0.5f,_YOffset;
    private void UpdateRay()
    {
        RaycastHit2D _Hit = Physics2D.Raycast(new Vector3(ShipCollider.transform.position.x, ShipCollider.transform.position.y + 10f,0), Vector2.down, Mathf.Infinity);
        if (_Hit.transform != null)
        {
            _Hitpoint = new Vector3(_Hit.point.x, _Hit.point.y + _YOffset, 0);

        }
        else _Hitpoint = new Vector2(ShipCollider.transform.position.x, ShipCollider.transform.position.y);
        
    }
    private void UpdatePosition()
    {
        transform.position = Vector3.Lerp(transform.position, _Hitpoint,MoveTime);
    }

    private void Start()
    {
        ShipCollider = GameObject.FindGameObjectWithTag("ShipChaser");

    }
    private void FixedUpdate()
    {
        UpdateRay();
        UpdatePosition();
    }
}
