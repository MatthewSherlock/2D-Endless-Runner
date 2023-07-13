using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    [SerializeField]
    GameObject[] _BackgroundPrefabs;
    public List<GameObject> _ActiveBackgroundSlots;
    [SerializeField]
    private float _WorldMoveSpeed = 1f;
    
    private int _BackgroundSlotsNeeded = 2;
    private float _PlayerXDeletionPoint;
    private Camera _MainCamera;


    [SerializeField]
    private float _PlayerXDeletion;
    [SerializeField]
    private float _XOffset,_YOffset, _ParallaxEffect;
    private void StartingGeneration()
    {

        float startingX = -_MainCamera.transform.position.x - (_MainCamera.ScreenToWorldPoint(new Vector3(0, 0, 0)).x);
        for (int i = 0; i < _BackgroundSlotsNeeded; i++)
        {
            GameObject Slot = null;
            int _SlotType = Random.Range(-3, _BackgroundPrefabs.Length); // Get Type to spawn
            if (_SlotType < 0)
                _SlotType = 0;

            Slot = Instantiate(_BackgroundPrefabs[_SlotType], new Vector3(-startingX + (_XOffset * _ActiveBackgroundSlots.Count), _YOffset, 0), new Quaternion(0, 0, 0, 0), GameObject.FindGameObjectWithTag("MainWorld").transform);
            Slot.transform.position -= new Vector3(18, 0, 0);

            _ActiveBackgroundSlots.Add(Slot);
        } // pointless comment
    }
    private void Generation()
    {
        if (_ActiveBackgroundSlots.Count < _BackgroundSlotsNeeded)
        {
            int _SlotType = Random.Range(-5, _BackgroundPrefabs.Length); // Get Type to spawn
            if (_SlotType < 0)
                _SlotType = 0;
            //Debug.Log(_SlotType);

            float XPos = 0;
            if (_ActiveBackgroundSlots.Count == 0)
                XPos = -_MainCamera.transform.position.x - _MainCamera.ScreenToWorldPoint(new Vector3(Screen.height / 2, 0, 0)).x; // spawning if none
            else
                XPos = _ActiveBackgroundSlots[_ActiveBackgroundSlots.Count - 1].transform.position.x + _XOffset; // Spawning on the end of the last object


            //var Slot = Instantiate(_WorldPrefabs[_SlotType], new Vector3(XPos, _WorldYLevel, 0), new Quaternion(0, 0, 0, 0), GameObject.FindGameObjectWithTag("MainWorld").transform); // spawning
            var Slot = Instantiate(_BackgroundPrefabs[_SlotType], _ActiveBackgroundSlots[_ActiveBackgroundSlots.Count - 1].GetComponent<PrefabDetails>().EndPoint.transform.position, new Quaternion(0, 0, 0, 0), GameObject.FindGameObjectWithTag("MainWorld").transform); // spawning
            _ActiveBackgroundSlots.Add(Slot); // adding to list
        }
    }

    private void UpdateNeeded()
    {
        float CameraEndX = _MainCamera.ScreenToWorldPoint(new Vector3(_MainCamera.pixelWidth, 0)).x + 5f;
        if (CameraEndX > _ActiveBackgroundSlots[_ActiveBackgroundSlots.Count - 1].GetComponent<PrefabDetails>().EndPoint.transform.position.x)
        {
            _BackgroundSlotsNeeded++;
            Generation();
            UpdateNeeded();

        }
        else if (CameraEndX < _ActiveBackgroundSlots[_ActiveBackgroundSlots.Count - 1].GetComponent<PrefabDetails>().EndPoint.transform.position.x)
        {
            _BackgroundSlotsNeeded--;


        }
        _BackgroundSlotsNeeded = (int)Mathf.Clamp(_BackgroundSlotsNeeded, 2, Mathf.Infinity);
    }

        private void Movement()
    {

        foreach (GameObject _BackgroundSlot in _ActiveBackgroundSlots)
        {
            if (_BackgroundSlot != null)
            {
                _BackgroundSlot.transform.localPosition += new Vector3(-_WorldMoveSpeed * Time.deltaTime, 0, 0);
                if (_BackgroundSlot.transform.position.x < (_PlayerXDeletionPoint))
                    Destroy(_BackgroundSlot.gameObject);
            }
        }
        CheckList();
    }

    private void CheckList()
    {
        for (int i = _ActiveBackgroundSlots.Count - 1; i >= 0; i--)
        {
            if (_ActiveBackgroundSlots[i] == null)
                _ActiveBackgroundSlots.RemoveAt(i);
        }
    }

    private void UpdateDeletionPoint()
    {
        _PlayerXDeletionPoint = (Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x + _PlayerXDeletion);
    }

    // Start is called before the first frame update
    void Start()
    {
        _MainCamera = Camera.main.GetComponent<Camera>();
        
        _ActiveBackgroundSlots = new List<GameObject>();
        StartingGeneration();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDeletionPoint();
        _WorldMoveSpeed = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().velocity.x * Time.deltaTime;
        Generation();
        Movement();
        UpdateDeletionPoint();
        UpdateNeeded();
        //CheckList();
    }
}
