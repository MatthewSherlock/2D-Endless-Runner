using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeWorldGen : MonoBehaviour
{
    [SerializeField]
    private GameObject _StartingWave;

    
    GameObject[][] _WorldPrefabs;
    [SerializeField]
    GameObject[] _BaseWaves,_HighWaves,_LowWaves;
    

    List<GameObject> _ActiveWorldSlots;
    [SerializeField]
    private float _WorldMoveSpeed = 1f;
    
    private int _WorldSlotsNeeded = 5;
    [SerializeField]
    private float _WorldYLevel,_PlayerXDeletion;
    private float _PlayerXDeletionPoint;
    private Camera _MainCamera;
    
    [SerializeField]
    private float _WorldShift;
    

    private void ChooseWaveType(ref int X,ref int Y)
    {

        int i = Random.Range(0, 3);
        int a = Random.Range(0, _WorldPrefabs[i].Length);

        if (_ActiveWorldSlots[_ActiveWorldSlots.Count - 1].GetComponent<PrefabDetails>().ID == _WorldPrefabs[i][a].GetComponent<PrefabDetails>().ID)
        {
            if(a == _WorldPrefabs[i].Length - 1)
            {
                a--;
            }
            else if(a == 0)
            {
                a++;
            }
            else
            {
                if (Random.Range(0, 2) == 0)
                    a--;
                else
                    a ++;
            }
                
        }
        // High wave cant spawn next too another high wave or default wave.
        if (i == 1 && _ActiveWorldSlots[_ActiveWorldSlots.Count - 1].GetComponent<PrefabDetails>().Type == PrefabDetails.WaveType.HighWave)
        {
            if (Random.Range(0, 2) == 0)
                i = 0;
            else
                i = 2;

            if (a >= _WorldPrefabs[i].Length)
            
                a = Random.Range(0, _WorldPrefabs[i].Length);
            
        }
        X = i;
        Y = a;
        
    }

    private void StartingGeneration()
    {

        float startingX = (_MainCamera.ScreenToWorldPoint(new Vector3(0, 0, 0)).x);
        for (int i = 0; i < _WorldSlotsNeeded; i++)
        {
            GameObject Slot = null;

            if (i == 0)
            {
                Slot = Instantiate(_StartingWave, new Vector3(+3, _WorldYLevel, 0), new Quaternion(0, 0, 0, 0), GameObject.FindGameObjectWithTag("MainWorld").transform);
                
            }

            else
            {
                Slot = Instantiate(_WorldPrefabs[0][0], _ActiveWorldSlots[_ActiveWorldSlots.Count - 1].GetComponent<PrefabDetails>().EndPoint.transform.position, new Quaternion(0, 0, 0, 0), GameObject.FindGameObjectWithTag("MainWorld").transform);
                
            }

            
            
            _ActiveWorldSlots.Add(Slot);
        } // pointless comment
    }
    private void Generation()
    {
        if(_ActiveWorldSlots.Count < _WorldSlotsNeeded)
        {
            int X = 0;
            int Y = 0;
            ChooseWaveType(ref X, ref Y);
            //Debug.Log(_SlotType);
            /*
            float XPos = 0;
            if (_ActiveWorldSlots.Count == 0)
                XPos = -_MainCamera.transform.position.x - _MainCamera.ScreenToWorldPoint(new Vector3(Screen.height / 2, 0, 0)).x; // spawning if none
            else
                XPos = _ActiveWorldSlots[_ActiveWorldSlots.Count - 1].transform.position.x + _XOffset; // Spawning on the end of the last object
            */

            //var Slot = Instantiate(_WorldPrefabs[_SlotType], new Vector3(XPos, _WorldYLevel, 0), new Quaternion(0, 0, 0, 0), GameObject.FindGameObjectWithTag("MainWorld").transform); // spawning
            var Slot = Instantiate(_WorldPrefabs[X][Y],_ActiveWorldSlots[_ActiveWorldSlots.Count - 1].GetComponent<PrefabDetails>().EndPoint.transform.position , new Quaternion(0, 0, 0, 0), GameObject.FindGameObjectWithTag("MainWorld").transform); // spawning
            _ActiveWorldSlots.Add(Slot); // adding to list
        }
    }

    private void Movement()
    {

        foreach (GameObject _WorldSlot in _ActiveWorldSlots)
        {
            if (_WorldSlot != null) { 
                _WorldSlot.transform.localPosition += new Vector3(-_WorldMoveSpeed * Time.deltaTime, 0, 0);
            if (_WorldSlot.transform.position.x < (_PlayerXDeletionPoint))            
                Destroy(_WorldSlot.gameObject);
             }
        }
    }

   
    private void UpdateNeeded()
    {
        float CameraEndX = _MainCamera.ScreenToWorldPoint(new Vector3(_MainCamera.pixelWidth, 0)).x + 5f;
        if (CameraEndX > _ActiveWorldSlots[_ActiveWorldSlots.Count - 1].GetComponent<PrefabDetails>().EndPoint.transform.position.x)
        {
            _WorldSlotsNeeded++;
            Generation();
            UpdateNeeded();
            
        }
        else if (CameraEndX < _ActiveWorldSlots[_ActiveWorldSlots.Count - 1].GetComponent<PrefabDetails>().EndPoint.transform.position.x)
        {
            _WorldSlotsNeeded--;

            
        }
        _WorldSlotsNeeded = (int)Mathf.Clamp(_WorldSlotsNeeded, 5, Mathf.Infinity);
        
    }

    private void CheckList()
    {
        for (int i = _ActiveWorldSlots.Count - 1; i >= 0; i--)
        {
            if (_ActiveWorldSlots[i] == null)
                _ActiveWorldSlots.RemoveAt(i);
        }
    }

    public void ShiftWorld()
    {
        GameObject.FindGameObjectWithTag("MainWorld").transform.position -= new Vector3(_WorldShift, 0, 0);
    }


    private void UpdateDeletionPoint()
    {
        _PlayerXDeletionPoint = (Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x + _PlayerXDeletion);
    }
    // Start is called before the first frame update
    void Start()
    {
        
        _MainCamera = Camera.main.GetComponent<Camera>();
        
        _ActiveWorldSlots = new List<GameObject>();
        
        _WorldPrefabs = new GameObject[3][];
        _WorldPrefabs[0] = new GameObject[_BaseWaves.Length];
        _WorldPrefabs[1] = new GameObject[_HighWaves.Length];
        _WorldPrefabs[2] = new GameObject[_LowWaves.Length];
        for (int i = 0; i < _BaseWaves.Length; i++)
        {
            _WorldPrefabs[0][i] = _BaseWaves[i];
        }
        for (int i = 0; i < _HighWaves.Length; i++)
        {
            _WorldPrefabs[1][i] = _HighWaves[i];
        }
        for (int i = 0; i < _LowWaves.Length; i++)
        {
            _WorldPrefabs[2][i] = _LowWaves[i];
        }
        StartingGeneration();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDeletionPoint();
        

        Generation();
        Movement();
        CheckList();
        UpdateNeeded();
        
    }

}
