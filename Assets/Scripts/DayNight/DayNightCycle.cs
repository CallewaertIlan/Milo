using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [SerializeField] private float timeForDay;
    [SerializeField] private float timeForNight;
    public int daysInGame;
    public int hoursInGame = 8;
    public float minutesInGame;


    public bool isDay = true;


    private static DayNightCycle _instance;
    public static DayNightCycle Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this);
        }

    }

    // Update is called once per frame
    void Update()
    {
        refreshTime();

        refreshSunPosition();
    }

    private bool IsDayOrNight()
    {
        if (hoursInGame <= 19 && hoursInGame >= 8) return isDay = true;
        return isDay = false;
    }

    private void refreshTime()
    {
        if (IsDayOrNight()) minutesInGame += Time.deltaTime * 720 / timeForDay;
        else minutesInGame += Time.deltaTime * 720 / timeForNight;

        if (minutesInGame >= 60)
        {
            minutesInGame -= 60;
            hoursInGame++;
        }

        if (hoursInGame >= 24)
        {
            hoursInGame -= 24;
            daysInGame++;
        }
    }

    private void refreshSunPosition()
    {
        if (isDay) transform.Rotate(Time.deltaTime * 180 / timeForDay, 0, 0);
        else transform.Rotate(Time.deltaTime * 180 / timeForNight, 0, 0);
    }
}
