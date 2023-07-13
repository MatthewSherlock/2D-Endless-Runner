using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 _Hitpoint;
    [SerializeField,InspectorName("Starting Gravity")]
    private float _GravityValue = 2f;
    [SerializeField]
    private float _SpeedGainValue = 0.25f, _SpeedDecayValue = 0.2f;
    [SerializeField]
    private float _MinSpeed = 0.3f, _MaxSpeed = 5f;
    public Vector3 _LastPoisition;
    [SerializeField]
    private float _MoveSpeedValue = 1f;
    [SerializeField]
    private bool _GoingDown = false;
    [SerializeField]
    private float _GravityAffect;

    public float GetPlayerSpeed()
    {
        return _MoveSpeedValue;
    }
    private void UpdateHitPoint()
    {
        RaycastHit2D _Hit = Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity);
        _Hitpoint = _Hit.point;
        if(_Hit.transform != null)
        {
            transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, _Hitpoint.y+0.5f, 5));
        }
        else
        {
            transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y,-6, 5));
        }
    }

    private void SlopeCollision()
    {
        Debug.DrawRay(transform.position, Vector2.down + Vector2.right);
        RaycastHit2D _Hit = Physics2D.Raycast(transform.position, Vector2.down + Vector2.right, 0.5f);
        if(_Hit.transform != null && transform.position.y < _LastPoisition.y)
        {
            Debug.Log("Slope Collision");
            _MoveSpeedValue *= (2 * Time.deltaTime);
            _MoveSpeedValue = Mathf.Clamp(_MoveSpeedValue, _MinSpeed, _MaxSpeed);
        }
    }

    private void GravityAffect(float MultiplierValue)
    {

        
        if (!_GoingDown)
        {
            _GravityAffect += _MoveSpeedValue  * Time.deltaTime;
        }
        else
        {
            _GravityAffect -= (_GravityValue - _GravityAffect) * MultiplierValue * Time.deltaTime;
        }

        _GravityAffect = Mathf.Clamp(_GravityAffect, -_GravityValue, _GravityValue);
        transform.position -= new Vector3(0, _GravityAffect * Time.deltaTime,0); // Apply Gravity
    }
    private void PlayerControls()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            transform.position -= new Vector3(0, _GravityValue * Time.deltaTime, 0);
        }
    }

    private void UpdateSpeed()
    {
        //_MoveSpeedValue -= _SpeedDecayValue * Time.deltaTime;
        if (_GoingDown) // Going Down
        {
            Debug.Log("Increasing Speed");
            if(Input.GetKey(KeyCode.S))
                _MoveSpeedValue += (_SpeedGainValue * 2) * Time.deltaTime;
            else _MoveSpeedValue += _SpeedGainValue * Time.deltaTime;
        }
        else  // Going Up
        {
            Debug.Log("Decreasing Speed");
            if (Input.GetKey(KeyCode.S))
            {
                _MoveSpeedValue -= (_SpeedDecayValue * 2) * Time.deltaTime;
                GravityAffect(2);
            }
            else _MoveSpeedValue -= _SpeedDecayValue * Time.deltaTime;
        }
        _MoveSpeedValue = Mathf.Clamp(_MoveSpeedValue, _MinSpeed, _MaxSpeed);

    }
    // Start is called before the first frame update
    void Start()
    {
        _LastPoisition = transform.position;
        _GravityAffect = _GravityValue;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position.y < _LastPoisition.y)
            _GoingDown = true;
        else
            _GoingDown = false;
        _LastPoisition = transform.position;

        GravityAffect(1);
        UpdateHitPoint();
        PlayerControls();
        SlopeCollision();
        UpdateSpeed();
        
    }
}
