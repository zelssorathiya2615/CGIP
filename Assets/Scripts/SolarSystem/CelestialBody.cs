// using UnityEngine;

// public class CelestialBody : MonoBehaviour
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

// public class CelestialBody : MonoBehaviour
// {
//     public float rotationSpeed = 10f;

//     private void Update()
//     {
//         transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
//     }
// }



using System;
using UnityEngine;

public class CelestialBody : MonoBehaviour
{
    [Header("Scientific Rotation Data")]
    [Tooltip("Sidereal rotation period in Earth days. Use a positive value.")]
    public double rotationPeriodDays = 1.0;

    [Tooltip("Approximate axial tilt relative to the orbital plane, in degrees.")]
    public float axialTiltDegrees = 0f;

    private TimeController timeController;
    private DateTime previousSimulationTime;
    private double accumulatedRotationDegrees;
    private Quaternion tiltRotation;

    private void Start()
    {
        timeController = FindAnyObjectByType<TimeController>();

        if (timeController == null)
        {
            Debug.LogError(
                "CelestialBody could not find a TimeController.", this);
            enabled = false;
            return;
        }

        if (rotationPeriodDays <= 0.0)
        {
            Debug.LogError(
                "Rotation period must be greater than zero.", this);
            enabled = false;
            return;
        }

        previousSimulationTime = timeController.simulationDateTime;

        // Tilt the spin axis relative to the orbital plane.
        tiltRotation = Quaternion.Euler(axialTiltDegrees, 0f, 0f);
        transform.localRotation = tiltRotation;
    }

    private void Update()
    {
        if (timeController == null)
            return;

        DateTime currentSimulationTime =
            timeController.simulationDateTime;

        double elapsedSimulationDays =
            (currentSimulationTime - previousSimulationTime).TotalDays;

        previousSimulationTime = currentSimulationTime;

        // A signed date difference automatically handles forward,
        // reverse, and paused simulation time.
        accumulatedRotationDegrees +=
            (elapsedSimulationDays / rotationPeriodDays) * 360.0;

        float spinAngle =
            (float)(accumulatedRotationDegrees % 360.0);

        transform.localRotation =
            tiltRotation * Quaternion.AngleAxis(spinAngle, Vector3.up);
    }
}
