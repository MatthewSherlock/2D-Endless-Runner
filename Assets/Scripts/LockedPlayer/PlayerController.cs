using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField,Range(0.1f,10f)]
    private float _DropSpeed = 1f;
    private float _MoveSpeed = 1f;
    public float _GravityValue = -1f;
    [SerializeField]
    private float _GravityDecay = -1f;
    [SerializeField]
    private float _MoveSpeedIncrease = 1f, _MoveSpeedDecrease = 1f;
    //[SerializeField]
    //private bool _EnableMovement = true;
    private Rigidbody2D _Rb;
    private Vector2 _LastPosition;
    private Vector2 _LastHitPosition;

    public float GetPlayerSpeed()
    {
        return _MoveSpeed;
    }

    private void Drop()
    {
        _Rb.AddForce(new Vector2(0, _DropSpeed * Time.deltaTime), ForceMode2D.Force);
    }
    private void Movement()
    {
        Vector2 _NewPosition = new Vector2(transform.position.x, transform.position.y);
        if (_NewPosition.y > _LastPosition.y)
        {
            _MoveSpeed -= _MoveSpeedDecrease * Time.deltaTime;
            _Rb.AddForce(new Vector2(0, -_DropSpeed * Time.deltaTime), ForceMode2D.Force);
        }
        else if (_NewPosition.y < _LastPosition.y)
        {
            _MoveSpeed += _MoveSpeedIncrease * Time.deltaTime;
            _Rb.AddForce(new Vector2(0, _DropSpeed * Time.deltaTime), ForceMode2D.Force);
        }
        _MoveSpeed = Mathf.Clamp(_MoveSpeed, 0.3f, 20f);
        _LastPosition = transform.position;
    }



    private void MovementMethod2()
    {
        Vector2 _GravityPosition = new Vector2(transform.position.x, transform.position.y - _GravityValue);
        RaycastHit2D _Hit = Physics2D.Raycast(transform.position, -Vector2.up,.5f);
        if(_Hit.transform != null)
        {
            ValueChange(_LastHitPosition.y);
            _LastHitPosition = _Hit.point;
        }
        else
        {
            ValueChange(0);
        }
        
        
     //   if(_Hit != null)
           // _GravityPosition.y = Mathf.Clamp(transform.position.y, _Hit.point.y, 999);
        float YChange = transform.position.y;
        YChange = Mathf.Lerp(YChange, _GravityPosition.y,1);
        transform.position -= new Vector3(0, YChange,0);
    }

    private void ValueChange(float HitDiff)
    {
        if (HitDiff < 0)
            _GravityValue += HitDiff;
        else if (HitDiff > 0 && Input.GetKey(KeyCode.Space))
            _MoveSpeed -= HitDiff;

        _GravityValue += _GravityDecay * Time.deltaTime;

    }

    // Start is called before the first frame update
    void Start()
    {
       // _Rb = GetComponent<Rigidbody2D>();
        _LastPosition = new Vector2(transform.position.x,transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        MovementMethod2();
        Debug.Log(_MoveSpeed);
    }
}
