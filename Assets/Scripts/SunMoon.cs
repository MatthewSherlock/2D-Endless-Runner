using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunMoon : MonoBehaviour
{
    [SerializeField]
    private GameObject sunObject, moonObject, starsObject;
    [SerializeField]
    private Vector3 sunStartPos, moonStartPos;
    [SerializeField]
    private Vector3 sunEndPos, moonEndPos;
    [SerializeField]
    private bool IsDay, IsTwlight, IsNight;
    private bool CycleIsDay, CycleIsTwilight, CycleIsNight;
    private DayNightCycle Cycle;

    private void SunUp()
    {
        sunObject.GetComponent<Animator>().SetTrigger("SunUp");
        moonObject.GetComponent<Animator>().SetTrigger("SunDown");
        IsNight = true; 
        IsDay = false;
    }

    private void SunDown()
    {
        sunObject.GetComponent<Animator>().SetTrigger("SunDown");
        moonObject.GetComponent<Animator>().SetTrigger("SunUp");
        IsDay = true;
        IsNight = false;
    }

    private void UpdateCycle()
    {
        CycleIsDay = Cycle.isDay;
        CycleIsTwilight = Cycle.isTwilight;
        CycleIsNight = Cycle.isNight;
    }
    // Start is called before the first frame update
    void Start()
    {
        Cycle = GetComponent<DayNightCycle>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCycle();
        if (CycleIsNight && !IsNight)
            SunUp();
        if (CycleIsDay && !IsDay)
            SunDown();
    }
}
