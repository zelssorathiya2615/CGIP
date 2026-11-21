// using UnityEngine;

// public class DateInputController : MonoBehaviour
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



using System;
using UnityEngine;
using TMPro;

public class DateInputController : MonoBehaviour
{
    [Header("References")]
    public GameObject dateInputPanel;
    public TMP_InputField dateInput;
    public TMP_InputField timeInput;
    public TimeController timeController;

    public void OpenPanel()
    {
        dateInputPanel.SetActive(true);

        DateTime current = timeController.simulationDateTime;

        dateInput.text = current.ToString("dd/MM/yyyy");
        timeInput.text = current.ToString("HH:mm");
    }

    public void ApplyDate()
    {
        DateTime selectedDate;

        string input = dateInput.text + " " + timeInput.text;

        bool valid = DateTime.TryParseExact(
            input,
            "dd/MM/yyyy HH:mm",
            System.Globalization.CultureInfo.InvariantCulture,
            System.Globalization.DateTimeStyles.None,
            out selectedDate
        );

        if (!valid)
        {
            Debug.LogWarning("Invalid date or time. Use DD/MM/YYYY and HH:MM.");
            return;
        }

        timeController.SetSimulationDate(selectedDate);
        dateInputPanel.SetActive(false);
    }

    public void ClosePanel()
    {
        dateInputPanel.SetActive(false);
    }
}
