using System;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static Action OnMinuteChanged;
    public static Action OnHourChanged;
    public static Action OnDayChanged;

    public static int Minute;
    public static int Hour;
    public static int Day;

    private float minuteToRealTime = 0.5f;
    private float timer;

    void Start()
    {
        Minute = 50;
        Hour = 13;
        Day = 1;
        timer = minuteToRealTime;
    }


    void Update()
    {
        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            Minute++;
            OnMinuteChanged?.Invoke();
            if (Minute >= 60)
            {
                Hour++;
                OnHourChanged?.Invoke();
                Minute = 0;
                if (Hour == 12)
                {
                    Debug.Log("food service");
                }
                if (Hour == 18)
                {
                    Debug.Log("food delivery");
                }
                if (Hour >= 24)
                {
                    Day++;
                    OnDayChanged?.Invoke();
                    Hour = 0;
                }

            }
            timer =minuteToRealTime;
        }
    }
}
