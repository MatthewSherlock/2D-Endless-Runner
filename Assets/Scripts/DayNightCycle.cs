using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [SerializeField]
    private GameObject sunObject, moonObject;
    [SerializeField]
    private Vector3 sunStartPos, moonStartPos;

    public bool isSunInScene, isMoonInScene, isDay, isNight, isTwilight, removeSun;
    [SerializeField]
    private Vector3 sunEndPos, moonEndPos;
    [SerializeField]
    private float currTimeOfDay, timeScale = 100.0f;
    [SerializeField]
    private float dayEndTime, twilightEndTime, nightEndTime, lerpSpeed;
    [SerializeField]
    private Color skyDayColour, skyTwilightColour, skyNightColour, currSkyColour;
    [SerializeField]
    private GameObject backgroundSprite;
    [SerializeField]
    private Camera mainCamera;
    private SpriteRenderer backSpriteRenderer;
    private float timeElapsed;
    private bool doDayOnce;
    void changeSkyColour(Color a, Color b)
    {
        mainCamera.backgroundColor = Color.Lerp(a, b, timeElapsed / lerpSpeed);
        timeElapsed = Time.deltaTime;
    }

    void ChangeToTwilight()
    {
        Color temp = mainCamera.backgroundColor;
        changeSkyColour(temp, skyTwilightColour);
    }

    void ChangeToDay()
    {
        Color temp = mainCamera.backgroundColor;

        if (!doDayOnce)
            changeSkyColour(temp, skyDayColour);

    }

    void ChangeToNight()
    {
        Color temp = mainCamera.backgroundColor;
        changeSkyColour(temp, skyNightColour);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        if (backgroundSprite) backSpriteRenderer = backgroundSprite.GetComponent<SpriteRenderer>();
        currSkyColour = skyDayColour;
        //mainCamera.backgroundColor = currSkyColour;
        currTimeOfDay = 0;
    }
    
    // Update is called once per frame
    void Update()
    {
        isNight = false;
        isTwilight = false;
        isDay = false;

        currTimeOfDay += Time.deltaTime * timeScale / 86400;

        if (currTimeOfDay <= dayEndTime)
        {
            isNight = false;
            isTwilight = false;
            isDay = true;
            isSunInScene = true;
            isMoonInScene = false;
        }


        if (currTimeOfDay > dayEndTime && currTimeOfDay < nightEndTime)
        {
            isDay = false;
            isNight = false;
            isTwilight = true;
            isSunInScene = false;
            //   StartCoroutine("LerpSun");
        }



        if (currTimeOfDay <= nightEndTime && currTimeOfDay > twilightEndTime)
        {
            isNight = true;
            isTwilight = false;
            isDay = false;
            isSunInScene = false;
            isMoonInScene = true;
        }

        if (currTimeOfDay >= 1) currTimeOfDay = 0.0f;

        currSkyColour = mainCamera.backgroundColor;

       // if (isDay) ChangeToDay();

       // if (!isDay) removeSun = true;

        // if (removeSun) RemoveSun();

        /// if (isTwilight) ChangeToTwilight();

        // if (isNight) ChangeToNight();

       // if (currTimeOfDay >= nightEndTime && currTimeOfDay <= 1) 
          //  ChangeToTwilight();


    }
}