using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabDetails : MonoBehaviour
{
    public GameObject EndPoint;

    public int ID;
    public enum WaveType { BaseWave, HighWave, LowWave };
    
    public WaveType Type;

}
