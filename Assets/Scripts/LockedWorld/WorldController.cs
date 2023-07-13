using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour
{

    [SerializeField]
    private GameObject[] _WorldSlots;
    [SerializeField]
    private float _MoveSpeed;
    [SerializeField]
    private int _WorldSlotsNum = 10;
    public List<GameObject> _ActiveWorldSlots;

    private void WorldMovement()
    {
        foreach (GameObject _WorldSlot in _ActiveWorldSlots)
        {
            _WorldSlot.transform.localPosition += new Vector3(-_MoveSpeed * Time.deltaTime, 0, 0);
            if (_WorldSlot.transform.position.x < -16f)
            {
                //ActiveWorldSlots.Remove(WorldSlot);
                Destroy(_WorldSlot);
            }
        }
    }

    private void WorldSpawning()
    {
        int _num = 0;//Random.Range(-5, _WorldSlots.Length);
        //int num = 0;
        if (_ActiveWorldSlots.Count < _WorldSlotsNum / 2 || _num < 0)
            _num = 0;
        GameObject _slot;
        if (_ActiveWorldSlots.Count == 0)
            _slot = Instantiate(_WorldSlots[_num], new Vector3(transform.localPosition.x, transform.position.y-4f, transform.localPosition.z), transform.rotation, gameObject.transform);
        else
        {
            _slot = Instantiate(_WorldSlots[_num], new Vector3(transform.localPosition.x + (_ActiveWorldSlots[_ActiveWorldSlots.Count - 1].transform.localPosition.x + 10), transform.localPosition.y-4f, transform.position.z), transform.rotation, gameObject.transform);
        }
        _ActiveWorldSlots.Add(_slot);
    }
    private void CheckList()
    {
        for (int i = _WorldSlots.Length - 1; i >= 0; i--)
        {
            if (_ActiveWorldSlots[i] == null)
                _ActiveWorldSlots.RemoveAt(i);
        }
    }

    private void WorldSetup()
    {
        for (int i = 0; i < _WorldSlotsNum; i++)
        {
            WorldSpawning();
        }
    }

    private void UpdateWorldSpeed()
    {
       if(GameObject.FindWithTag("Player") != null)
            _MoveSpeed = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().GetPlayerSpeed();
       
        
    }
    // Start is called before the first frame update
    void Start()
    {
        _ActiveWorldSlots = new List<GameObject>();
        WorldSetup();
    }



    // Update is called once per frame
    void Update()
    {
        if (_ActiveWorldSlots.Count < _WorldSlotsNum)
        {
            WorldSpawning();
        }
        CheckList();
        UpdateWorldSpeed();
        WorldMovement();

    }
}
