using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public float counter;

    public int timeMinute = 0;
    public int timeHour = 12;

    public int dateDay = 12;
    public int dateMonth = 2;

    public TextMeshProUGUI textTimeMinute;
    public TextMeshProUGUI textTimeHour;

    public TextMeshProUGUI textDateDay;
    public TextMeshProUGUI textDateMonth;

    public StatsManager statsManager;

    public bool isStop = false;

    void Update()
    {
        textTimeMinute.text = timeMinute.ToString();
        textTimeHour.text = timeHour.ToString();
        textDateDay.text = dateDay.ToString();
        textDateMonth.text = dateMonth.ToString();

        StartTimer();
    }

    //Counts the time and jumps to the next time unit at the maximum
    void StartTimer()
    {
        if (!isStop)
        {
            counter += Time.deltaTime * 6;
        }

        timeMinute = Mathf.RoundToInt(counter);

        if (timeMinute >= 60)
        {
            counter = 0;
            timeMinute = 0;
            timeHour++;
            statsManager.HourlyConsumption();
        }
        if (timeHour >= 24)
        {
            timeHour = 0;
            dateDay++;

        }
        if (dateDay >= 30)
        {
            dateDay = 1;
            dateMonth++;
        }
    }
}
