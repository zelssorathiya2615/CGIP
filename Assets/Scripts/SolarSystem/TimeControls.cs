// using UnityEngine;

// public class TimeControls : MonoBehaviour
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



// using TMPro;
// using UnityEngine;

// public class TimeControls : MonoBehaviour
// {
//     public TimeController timeController;
//     public TMP_Text dateTimeText;

//     private void Update()
//     {
//         if (timeController == null || dateTimeText == null)
//             return;

//         dateTimeText.text =
//             timeController.simulationDateTime.ToString(
//                 "HH:mm:ss dd-MM-yyyy"
//             );
//     }
// }



// using TMPro;
// using UnityEngine;
// using UnityEngine.UI;

// public class TimeControls : MonoBehaviour
// {
//     [Header("References")]
//     public TimeController timeController;
//     public TMP_Text dateTimeText;

//     [Header("Buttons")]
//     public Button liveButton;
//     public Button reverseButton;
//     public Button pauseButton;
//     public Button forwardButton;
//     public Button fasterButton;

//     private void Start()
//     {
//         if (timeController == null)
//         {
//             Debug.LogError("TimeController is not assigned.", this);
//             enabled = false;
//             return;
//         }

//         if (liveButton != null)
//             liveButton.onClick.AddListener(timeController.SetLiveTime);

//         if (reverseButton != null)
//             reverseButton.onClick.AddListener(
//                 () => timeController.SetSimulationRate(-1.0));

//         if (pauseButton != null)
//             pauseButton.onClick.AddListener(timeController.Pause);

//         if (forwardButton != null)
//             forwardButton.onClick.AddListener(
//                 () => timeController.SetSimulationRate(1.0));

//         if (fasterButton != null)
//             fasterButton.onClick.AddListener(
//                 () => timeController.SetSimulationRate(10.0));
//     }

//     private void Update()
//     {
//         if (timeController == null || dateTimeText == null)
//             return;

//         dateTimeText.text =
//             timeController.simulationDateTime.ToString(
//                 "HH:mm:ss dd-MM-yyyy");
//     }

//     private void OnDestroy()
//     {
//         if (liveButton != null)
//             liveButton.onClick.RemoveListener(timeController.SetLiveTime);

//         if (reverseButton != null)
//             reverseButton.onClick.RemoveAllListeners();

//         if (pauseButton != null)
//             pauseButton.onClick.RemoveListener(timeController.Pause);

//         if (forwardButton != null)
//             forwardButton.onClick.RemoveAllListeners();

//         if (fasterButton != null)
//             fasterButton.onClick.RemoveAllListeners();
//     }
// }




using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeControls : MonoBehaviour
{
    [Header("References")]
    public TimeController timeController;
    public TMP_Text dateTimeText;
    public TMP_Text rateText;

    // [Header("Buttons")]
    // public Button liveButton;
    // public Button reverseButton;
    // public Button pauseButton;
    // public Button forwardButton;
    // public Button fasterButton;


    [Header("Buttons")]
    public Button liveButton;
    public Button reverseButton;
    public Button pauseButton;
    public Button forwardButton;
    public Button slowerButton;
    public Button fasterButton;

    [Header("Speed Slider")]
    public Slider speedSlider;



    private void Start()
    {
        if (timeController == null)
        {
            Debug.LogError("TimeController is not assigned.", this);
            enabled = false;
            return;
        }

        // if (liveButton != null)
        //     liveButton.onClick.AddListener(timeController.SetLiveTime);

        // if (reverseButton != null)
        //     reverseButton.onClick.AddListener(
        //         () => timeController.SetDirection(-1));

        // if (pauseButton != null)
        //     pauseButton.onClick.AddListener(timeController.Pause);

        // if (forwardButton != null)
        //     forwardButton.onClick.AddListener(
        //         () => timeController.SetDirection(1));

        // if (fasterButton != null)
        //     fasterButton.onClick.AddListener(
        //         () => timeController.MultiplySpeed(10.0));



        
        if (liveButton != null)
            liveButton.onClick.AddListener(timeController.SetLiveTime);

        if (reverseButton != null)
            reverseButton.onClick.AddListener(
                () => timeController.SetDirection(-1));

        if (pauseButton != null)
            pauseButton.onClick.AddListener(timeController.Pause);

        if (forwardButton != null)
            forwardButton.onClick.AddListener(
                () => timeController.SetDirection(1));

        if (fasterButton != null)
            fasterButton.onClick.AddListener(timeController.NextRate);
        
        if (slowerButton != null)
            slowerButton.onClick.AddListener(timeController.PreviousRate);

        
        // if (speedSlider != null)
        // {
        //     speedSlider.minValue = 0;
        //     speedSlider.maxValue = 45;
        //     speedSlider.wholeNumbers = true;
        //     speedSlider.SetValueWithoutNotify(timeController.rateIndex);

        //     speedSlider.onValueChanged.AddListener(
        //         value => timeController.SetRateIndex((int)value));
        // }


        if (speedSlider != null)
        {
            speedSlider.minValue = -45;
            speedSlider.maxValue = 45;
            speedSlider.wholeNumbers = true;
            speedSlider.SetValueWithoutNotify(0);

            speedSlider.onValueChanged.AddListener(value =>
            {
                int index = Mathf.Abs((int)value);

                if ((int)value == 0)
                {
                    timeController.Pause();
                }
                else
                {
                    timeController.SetRateIndex(index);
                    timeController.SetDirection((int)value < 0 ? -1 : 1);
                }
            });
        }
        
    }

    private void Update()
    {
        if (timeController == null)
            return;

        if (dateTimeText != null)
        {
            dateTimeText.text =
                timeController.simulationDateTime.ToString(
                    "HH:mm:ss dd-MM-yyyy");
        }

        // if (rateText != null)
        //     rateText.text = GetRateLabel();

        if (rateText != null)
            rateText.text = timeController.CurrentRateLabel;
    }

    private string GetRateLabel()
    {
        if (timeController.IsLive)
            return "LIVE";

        double rate = timeController.simulationDaysPerSecond;

        if (rate == 0.0)
            return "PAUSED";

        double secondsPerSecond = Math.Abs(rate) * 86400.0;
        string unit;
        double value;

        if (secondsPerSecond < 60.0)
        {
            value = secondsPerSecond;
            unit = "SEC/S";
        }
        else if (secondsPerSecond < 3600.0)
        {
            value = secondsPerSecond / 60.0;
            unit = "MIN/S";
        }
        else if (secondsPerSecond < 86400.0)
        {
            value = secondsPerSecond / 3600.0;
            unit = "HRS/S";
        }
        else if (secondsPerSecond < 365.25 * 86400.0)
        {
            value = secondsPerSecond / 86400.0;
            unit = "DAYS/S";
        }
        else
        {
            value = secondsPerSecond / (365.25 * 86400.0);
            unit = "YRS/S";
        }

        string sign = rate < 0 ? "-" : "";
        return $"{sign}{value:0.##} {unit}";
    }
}
