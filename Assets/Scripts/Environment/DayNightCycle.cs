using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DayNightCycle : MonoBehaviour
{
    public static event System.Action<int> OnDayChanged;

    public float time = 0f; //0-.5 is day, .5-1 is night
    public float minutesInDay = 10f;
    public int currentDay = 1;

    // chatgpt for sun logic
    public Light sun;
    public Gradient sunColorGradient;
    public AnimationCurve sunIntensityCurve;

    // Update is called once per frame
    void Update()
    {
        // increment the time
        time += Time.deltaTime / (minutesInDay * 60f);
        if (time > 1f)
        {
            time -= 1f;
            // do new game logic here
            currentDay++;
            OnDayChanged?.Invoke(currentDay);
        }
        UpdateLighting();
    }

    //chatgpt for sun logic
    void UpdateLighting()
    {
        // Rotate sun
        float sunAngle = (time * 360f) - 90f;
        sun.transform.rotation = Quaternion.Euler(sunAngle, 170f, 0f);

        // Adjust light color and brightness
        sun.color = sunColorGradient.Evaluate(time);
        sun.intensity = sunIntensityCurve.Evaluate(time);
        
        // Optional: Update ambient color or fog
        RenderSettings.ambientLight = sunColorGradient.Evaluate(time);
    }
    
}
