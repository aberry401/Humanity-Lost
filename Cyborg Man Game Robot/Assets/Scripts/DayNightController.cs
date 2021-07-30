using UnityEngine;
using System.Collections;

public class DayNightController : MonoBehaviour
{
    public GameObject[] lights;








    public Light sun;
    public float secondsInFullDay = 120f;
    [Range(0, 1)]
    public float currentTimeOfDay = 0;
    [HideInInspector]
    public float timeMultiplier = 1f;

    bool lightOn;
    float sunInitialIntensity;

    void Start()
    {
        lightOn = false;
        sunInitialIntensity = sun.intensity;
         lights = GameObject.FindGameObjectsWithTag("nightLight");
        foreach(GameObject g in lights)
        {
            g.SetActive(false);
        }
    }

    void Update()
    {
        UpdateSun();

        currentTimeOfDay += (Time.deltaTime / secondsInFullDay) * timeMultiplier;

        if (currentTimeOfDay >= 1)
        {
            currentTimeOfDay = 0;
        }

        if (lightOn)
        {
            foreach (GameObject g in lights)
            {
                g.SetActive(true);
            }
        } else
        {
            foreach(GameObject g in lights)
            {
                g.SetActive(false);
            }
        }
    }

    void UpdateSun()
    {
        if (currentTimeOfDay <= 0.33f || currentTimeOfDay >= 0.75f)
        {
            lightOn = true;
        } else
        {
            lightOn = false;
        }


            sun.transform.localRotation = Quaternion.Euler((currentTimeOfDay * 360f) - 90, 170, 0);

        float intensityMultiplier = 1;
        if (currentTimeOfDay <= 0.23f || currentTimeOfDay >= 0.75f)
        {
           
               
            intensityMultiplier = 0;
        }
        else if (currentTimeOfDay <= 0.25f)
        {
           
            intensityMultiplier = Mathf.Clamp01((currentTimeOfDay - 0.23f) * (1 / 0.02f));
        }
        else if (currentTimeOfDay >= 0.73f)
        {
            
            intensityMultiplier = Mathf.Clamp01(1 - ((currentTimeOfDay - 0.73f) * (1 / 0.02f)));
        }

        sun.intensity = sunInitialIntensity * intensityMultiplier;
    }
}