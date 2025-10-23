using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DayCounter : MonoBehaviour
{
    public TMP_Text dayText;

    void OnEnable()
    {
        DayNightCycle.OnDayChanged += UpdateDayText;
    }

    void OnDisable()
    {
        DayNightCycle.OnDayChanged -= UpdateDayText;
    }

    void UpdateDayText(int newDay)
    {
        dayText.text = $"Day {newDay}";
    }
}
