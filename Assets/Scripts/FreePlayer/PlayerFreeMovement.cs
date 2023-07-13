using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class PlayerFreeMovement : MonoBehaviour
{

    private Rigidbody2D _Rigidbody;
    private Vector3 _Position;

    public bool Enabled = false;
    [SerializeField]
    private float _GravityValueMin = 0.5f, _GravityValueMax = 1f, _ChangeTime = 1f;
    private bool _Caught = false, _IsPressed;
    [SerializeField]
    internal float _MoveSpeed = 1f, _DropSpeed = 1f, _WorldShiftPosition = 100f, _JumpSpeed;
    [SerializeField]
    public GameObject _Hitpoint;
    public bool isInvincible, _isGrounded;
    private GameObject _Bubble;
    private bool _IsBubbled = false;
    [SerializeField]
    GameObject BubbleSound;

    private void UpdateHitPoint()
    {
        RaycastHit2D _Hit = Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity);
        if(Vector3.Distance(transform.position, _Hit.point) <= 0.3f)
        {
            _isGrounded = true;
        }
        else
        {
            _isGrounded = false;
        }
        _Hitpoint.transform.position = new Vector3(_Hit.point.x, -3);


    }

    private void ConstRightMovement()
    {
        // old line:   transform.position += new Vector3(_MoveSpeed * Time.deltaTime, 0, 0);
      //  float _NewMovSpeed = (_MoveSpeed + _MoveSpeed / 100 * DataManager._Instance._SpeedModifierPer); // adds the percentage buff to the speed
        transform.position += new Vector3(_MoveSpeed * Time.deltaTime, 0, 0);
    }
    public void Drop()
    {
        _Rigidbody.AddForce(Vector2.down * _DropSpeed * Time.deltaTime, ForceMode2D.Impulse);
           
    }

    public void UseStartBoost()
    {
        if(DataManager._Instance._StartingBoostBought >= 1)
        {
            DataManager._Instance._StartingBoostBought--;
            UpgradeManager._Instance.GetComponent<StartingBoost>().UseSpeedBoost();
            GameObject.FindGameObjectWithTag("StartButton").SetActive(false);
            GetComponent<AudioSource>().Play();
        }
    }

    public void Jump()
    {
        if (_isGrounded)
        {
            _Rigidbody.AddForce(Vector2.up * _JumpSpeed, ForceMode2D.Impulse);
        }
    }
    private void Tick()
    {
        /*
        if (Input.GetKey(KeyCode.Space) || Input.touchCount >= 1)
        {
            Drop();
        }
        */
        ConstRightMovement();

    }


    private void GravityChange()
    {
        if(_Rigidbody.velocity.y > 0)
        {
            _Rigidbody.gravityScale = Mathf.Lerp(_Rigidbody.gravityScale, _GravityValueMin, _ChangeTime);
        }
        else if (_Rigidbody.velocity.y < 0)
        {
            _Rigidbody.gravityScale = Mathf.Lerp(_Rigidbody.gravityScale, _GravityValueMax, _ChangeTime);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        _Rigidbody = GetComponent<Rigidbody2D>();
        _Position = transform.position;
        _Caught = false;
        _Bubble = GameObject.FindGameObjectWithTag("Bubble");
        if (DataManager._Instance._StartingBoostBought == 0)
        {
            GameObject.FindGameObjectWithTag("StartButton").SetActive(false);
        }
    }

    public void GetCaught()
    {
        _Caught = true;
        _Rigidbody.simulated = false;
    }


    public IEnumerator Invincible()
    {
        yield return new WaitForSeconds(DataManager._Instance._InvinDur);
        isInvincible = false;

    }

    public void ButtonDown()
    {
        _IsPressed = true;
    }
    public void ButtonUp()
    {
        _IsPressed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Enabled && !_Caught)
        {
            if (!_Rigidbody.simulated)
                _Rigidbody.simulated = true;

            Tick();
            if (transform.position.x >= _WorldShiftPosition)
                GetComponentInChildren<FreeWorldGen>().ShiftWorld();
            Vector3 Velocity = _Rigidbody.velocity;
            Velocity.x = Mathf.Clamp(Velocity.x, 0, Mathf.Infinity);
            _Rigidbody.velocity = Velocity;
        }
        else if (Input.GetKeyDown(KeyCode.Space) || Input.touchCount >= 1)
        {
                Enabled = true;
            //   _Rigidbody.AddForce(Vector2.up + Vector2.right * 3f, ForceMode2D.Impulse);

            if(GameObject.FindGameObjectWithTag("StartButton") != null)
                GameObject.FindGameObjectWithTag("StartButton").SetActive(false);


        }

        if (_IsPressed)
        {
            Drop();
        }
        UpdateHitPoint();
        GravityChange();
        if (isInvincible)
        {
            _Bubble.SetActive(true);
            if(!_IsBubbled)
            _Bubble.GetComponent<AudioSource>().Play();
            _IsBubbled = true;
        }
        else
        {
            if (_IsBubbled && !isInvincible)
            {
                _Bubble.GetComponent<AudioSource>().Stop();
                BubbleSound.GetComponent<AudioSource>().Play();

            }
            _IsBubbled = false;
            _Bubble.SetActive(false);
            
        }
        transform.localPosition = new Vector3(Mathf.Clamp(transform.localPosition.x, _Position.x, Mathf.Infinity), transform.localPosition.y, 0);
        _Position = transform.localPosition;


    }
}
