// using UnityEngine;

// public class TimeController : MonoBehaviour
// {
//     // Start is called once before the first execution of Update after the MonoBehaviour is created
//     void Start()
//     {
        
//     }

//     // Update is called once per frame
//     void Update()
//     {
        
//     }
// }


// using UnityEngine;

// public class TimeController : MonoBehaviour
// {
//     public float timeScale = 1f;

//     public float SimulationTime { get; private set; }

//     private void Update()
//     {
//         SimulationTime += Time.deltaTime * timeScale;
//     }
// }


// using System;
// using UnityEngine;

// public class TimeController : MonoBehaviour
// {
//     [Header("Simulation Time")]
//     public DateTime simulationDateTime;

//     [Header("Simulation Rate")]
//     public double simulationDaysPerSecond = 1.0;

//     public bool IsPaused => simulationDaysPerSecond == 0.0;

//     private void Start()
//     {
//         simulationDateTime = DateTime.Now;
//     }

//     private void Update()
//     {
//         double daysToAdvance =
//             simulationDaysPerSecond * Time.deltaTime;

//         simulationDateTime =
//             simulationDateTime.AddDays(daysToAdvance);
//     }

//     public void SetSimulationRate(double daysPerSecond)
//     {
//         simulationDaysPerSecond = daysPerSecond;
//     }

//     public void Pause()
//     {
//         simulationDaysPerSecond = 0.0;
//     }

//     public void SetLiveTime()
//     {
//         simulationDateTime = DateTime.Now;
//     }
// }


// using System;
// using UnityEngine;

// public class TimeController : MonoBehaviour
// {
//     [Header("Simulation Time")]
//     public DateTime simulationDateTime;

//     [Header("Simulation Rate")]
//     public double simulationDaysPerSecond = 1.0;

//     public bool IsPaused => simulationDaysPerSecond == 0.0;

//     public double JulianDate =>
//         global::JulianDate.FromDateTime(simulationDateTime);

//     private void Start()
//     {
//         simulationDateTime = DateTime.Now;
//     }

//     private void Update()
//     {
//         double daysToAdvance =
//             simulationDaysPerSecond * Time.deltaTime;

//         simulationDateTime =
//             simulationDateTime.AddDays(daysToAdvance);
//     }

//     public void SetSimulationRate(double daysPerSecond)
//     {
//         simulationDaysPerSecond = daysPerSecond;
//     }

//     public void Pause()
//     {
//         simulationDaysPerSecond = 0.0;
//     }

//     public void SetLiveTime()
//     {
//         simulationDateTime = DateTime.Now;
//     }
// }




// using System;
// using UnityEngine;

// public class TimeController : MonoBehaviour
// {
//     [Header("Simulation Time")]
//     public DateTime simulationDateTime;

//     [Header("Simulation Rate")]
//     [Tooltip("Signed simulated days per real second.")]
//     public double simulationDaysPerSecond = 1.0;

//     public bool IsPaused => !isLive && simulationDaysPerSecond == 0.0;
//     public bool IsLive => isLive;

//     private bool isLive = true;

//     public double JulianDate =>
//         global::JulianDate.FromDateTime(simulationDateTime);

//     private void Start()
//     {
//         simulationDateTime = DateTime.Now;
//         isLive = true;
//     }

//     private void Update()
//     {
//         if (isLive)
//         {
//             simulationDateTime = DateTime.Now;
//             return;
//         }

//         double daysToAdvance =
//             simulationDaysPerSecond * Time.deltaTime;

//         simulationDateTime =
//             simulationDateTime.AddDays(daysToAdvance);
//     }

//     public void SetSimulationRate(double daysPerSecond)
//     {
//         isLive = false;
//         simulationDaysPerSecond = daysPerSecond;
//     }

//     public void Pause()
//     {
//         isLive = false;
//         simulationDaysPerSecond = 0.0;
//     }

//     public void SetLiveTime()
//     {
//         simulationDateTime = DateTime.Now;
//         isLive = true;
//     }
// }




// using System;
// using UnityEngine;

// public class TimeController : MonoBehaviour
// {
//     [Header("Simulation Time")]
//     public DateTime simulationDateTime;

//     [Header("Simulation Rate")]
//     [Tooltip("Signed simulated days per real second.")]
//     public double simulationDaysPerSecond = 1.0;

//     private bool isLive = true;
//     private int lastDirection = 1;

//     private const double RealRateDaysPerSecond = 1.0 / 86400.0;

//     public bool IsPaused => !isLive && simulationDaysPerSecond == 0.0;
//     public bool IsLive => isLive;

//     public double JulianDate =>
//         global::JulianDate.FromDateTime(simulationDateTime);

//     public double CurrentRateDaysPerSecond =>
//         isLive ? RealRateDaysPerSecond : simulationDaysPerSecond;

//     private void Start()
//     {
//         simulationDateTime = DateTime.Now;
//         isLive = true;
//     }

//     private void Update()
//     {
//         if (isLive)
//         {
//             simulationDateTime = DateTime.Now;
//             return;
//         }

//         double daysToAdvance =
//             simulationDaysPerSecond * Time.deltaTime;

//         simulationDateTime =
//             simulationDateTime.AddDays(daysToAdvance);
//     }

//     public void SetSimulationRate(double daysPerSecond)
//     {
//         isLive = false;
//         simulationDaysPerSecond = daysPerSecond;

//         if (daysPerSecond > 0)
//             lastDirection = 1;
//         else if (daysPerSecond < 0)
//             lastDirection = -1;
//     }

//     // public void SetDirection(int direction)
//     // {
//     //     direction = direction < 0 ? -1 : 1;

//     //     double magnitude = isLive
//     //         ? RealRateDaysPerSecond
//     //         : Math.Abs(simulationDaysPerSecond);

//     //     if (magnitude == 0.0)
//     //         magnitude = 1.0;

//     //     lastDirection = direction;
//     //     SetSimulationRate(direction * magnitude);
//     // }


//     public void SetDirection(int direction)
//     {
//         direction = direction < 0 ? -1 : 1;

//         double magnitude = Math.Abs(simulationDaysPerSecond);

//         // Forward or Reverse should work directly from LIVE or Pause.
//         if (isLive || magnitude == 0.0)
//             magnitude = 1.0;

//         SetSimulationRate(direction * magnitude);
//     }



//     // public void MultiplySpeed(double multiplier)
//     // {
//     //     if (multiplier <= 0.0)
//     //         return;

//     //     double magnitude = isLive
//     //         ? RealRateDaysPerSecond
//     //         : Math.Abs(simulationDaysPerSecond);

//     //     if (magnitude == 0.0)
//     //         magnitude = RealRateDaysPerSecond;

//     //     double newRate = magnitude * multiplier * lastDirection;

//     //     SetSimulationRate(newRate);
//     // }

    
//     public void MultiplySpeed(double multiplier)
//     {
//         if (multiplier <= 0.0)
//             return;

//         double magnitude = Math.Abs(simulationDaysPerSecond);

//         // If LIVE or paused, begin at one simulated day per second.
//         if (isLive || magnitude == 0.0)
//             magnitude = 1.0;

//         int direction = simulationDaysPerSecond < 0
//             ? -1
//             : lastDirection;

//         SetSimulationRate(direction * magnitude * multiplier);
//     }



//     public void Pause()
//     {
//         if (simulationDaysPerSecond > 0)
//             lastDirection = 1;
//         else if (simulationDaysPerSecond < 0)
//             lastDirection = -1;

//         isLive = false;
//         simulationDaysPerSecond = 0.0;
//     }

//     public void SetLiveTime()
//     {
//         simulationDateTime = DateTime.Now;
//         isLive = true;
//     }
// }






using System;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    [Header("Simulation Time")]
    public DateTime simulationDateTime;

    [Header("Rate Presets")]
    public int rateIndex = 0;

    private bool isLive = true;
    private bool isPaused = false;
    private int lastDirection = 1;

    // Rates are stored as simulated days per real second.
    // Months and years use average astronomical durations.
    private static readonly string[] RateLabels =
    {
        "3 sec/s", "5 sec/s", "6 sec/s", "8 sec/s",
        "10 sec/s", "20 sec/s", "30 sec/s", "40 sec/s",
        "50 sec/s",
        "1 min/s", "3 min/s", "5 min/s", "6 min/s",
        "8 min/s", "10 min/s", "20 min/s", "30 min/s",
        "40 min/s", "50 min/s",
        "1 hr/s", "3 hr/s", "5 hr/s", "6 hr/s",
        "8 hr/s", "10 hr/s", "13 hr/s", "16 hr/s",
        "18 hr/s", "21 hr/s",
        "1 day/s", "2 day/s", "3 day/s", "5 day/s",
        "6 day/s",
        "1 week/s", "3 week/s",
        "1 month/s", "2 month/s", "4 month/s",
        "6 month/s", "8 month/s", "10 month/s",
        "1 yr/s", "2 yr/s", "3 yr/s", "5 yr/s"
    };

    private static readonly double[] RateDaysPerSecond =
    {
        3.0 / 86400, 5.0 / 86400, 6.0 / 86400, 8.0 / 86400,
        10.0 / 86400, 20.0 / 86400, 30.0 / 86400, 40.0 / 86400,
        50.0 / 86400,
        60.0 / 86400, 180.0 / 86400, 300.0 / 86400,
        360.0 / 86400, 480.0 / 86400, 600.0 / 86400,
        1200.0 / 86400, 1800.0 / 86400, 2400.0 / 86400,
        3000.0 / 86400,
        3600.0 / 86400, 10800.0 / 86400, 18000.0 / 86400,
        21600.0 / 86400, 28800.0 / 86400, 36000.0 / 86400,
        46800.0 / 86400, 57600.0 / 86400, 64800.0 / 86400,
        75600.0 / 86400,
        1.0, 2.0, 3.0, 5.0, 6.0,
        7.0, 21.0,
        30.436875, 60.87375, 121.7475,
        182.62125, 243.495, 304.36875,
        365.2425, 730.485, 1095.7275, 1826.2125
    };

    public bool IsLive => isLive;
    public bool IsPaused => isPaused;

    public double simulationDaysPerSecond { get; private set; }

    public double JulianDate =>
        global::JulianDate.FromDateTime(simulationDateTime);

    public string CurrentRateLabel =>
        isLive ? "LIVE" :
        isPaused ? "PAUSED" :
        (lastDirection < 0 ? "-" : "") + RateLabels[rateIndex];

    private void Start()
    {
        simulationDateTime = DateTime.Now;
        rateIndex = Mathf.Clamp(rateIndex, 0, RateLabels.Length - 1);
        simulationDaysPerSecond = RateDaysPerSecond[rateIndex];
        isLive = true;
        isPaused = false;
    }

    private void Update()
    {
        if (isLive)
        {
            simulationDateTime = DateTime.Now;
            return;
        }

        if (isPaused)
            return;

        simulationDateTime = simulationDateTime.AddDays(
            simulationDaysPerSecond * lastDirection * Time.deltaTime
        );
    }

    public void SetSimulationRate(double daysPerSecond)
    {
        isLive = false;
        isPaused = false;

        if (daysPerSecond < 0)
            lastDirection = -1;
        else if (daysPerSecond > 0)
            lastDirection = 1;

        simulationDaysPerSecond = Math.Abs(daysPerSecond);
    }

    public void SetDirection(int direction)
    {
        if (direction == 0)
            return;

        lastDirection = direction < 0 ? -1 : 1;
        isLive = false;
        isPaused = false;
    }

    public void MultiplySpeed(double multiplier)
    {
        if (multiplier > 1.0)
            NextRate();
        else if (multiplier > 0.0 && multiplier < 1.0)
            PreviousRate();
    }

    public void NextRate()
    {
        rateIndex = Mathf.Min(rateIndex + 1, RateLabels.Length - 1);
        simulationDaysPerSecond = RateDaysPerSecond[rateIndex];
        isLive = false;
        isPaused = false;
    }

    // public void PreviousRate()
    // {
    //     rateIndex = Mathf.Max(rateIndex - 1, 0);
    //     simulationDaysPerSecond = RateDaysPerSecond[rateIndex];
    //     isLive = false;
    //     isPaused = false;
    // }

    public void PreviousRate()
    {
        rateIndex = Mathf.Max(rateIndex - 1, 0);
        simulationDaysPerSecond = RateDaysPerSecond[rateIndex];
        isLive = false;
        isPaused = false;
    }

    public void SetRateIndex(int index)
    {
        rateIndex = Mathf.Clamp(index, 0, RateDaysPerSecond.Length - 1);
        simulationDaysPerSecond = RateDaysPerSecond[rateIndex];
        isLive = false;
        isPaused = false;
    }


    public void Pause()
    {
        isLive = false;
        isPaused = true;
    }

    public void SetLiveTime()
    {
        simulationDateTime = DateTime.Now;
        isLive = true;
        isPaused = false;
    }


    public void SetSimulationDate(DateTime selectedDate)
    {
        simulationDateTime = selectedDate;
        isLive = false;
        isPaused = true;
    }



}
