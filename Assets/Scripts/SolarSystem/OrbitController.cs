// using UnityEngine;

// public class OrbitController : MonoBehaviour
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

// This script controls the orbit of a celestial body around a central point (e.g., a star or planet).
// It calculates the position of the orbiting body based on the specified orbit radius and speed, updating its position each frame to create a smooth orbital motion.
// script version 1.0
// using UnityEngine;

// public class OrbitController : MonoBehaviour
// {
//     public Transform center;
//     public float orbitRadius = 10f;
//     public float orbitSpeed = 10f;

//     private float currentAngle;

//     private void Start()
//     {
//         currentAngle = Mathf.Atan2(
//             transform.position.z - center.position.z,
//             transform.position.x - center.position.x
//         ) * Mathf.Rad2Deg;
//     }

//     private void Update()
//     {
//         currentAngle += orbitSpeed * Time.deltaTime;

//         float angleRadians = currentAngle * Mathf.Deg2Rad;

//         float x = center.position.x + Mathf.Cos(angleRadians) * orbitRadius;
//         float z = center.position.z + Mathf.Sin(angleRadians) * orbitRadius;

//         transform.position = new Vector3(
//             x,
//             transform.position.y,
//             z
//         );
//     }
// }


//script version 2.0
// using UnityEngine;

// public class OrbitController : MonoBehaviour
// {
//     public Transform center;
//     public float orbitRadius = 10f;
//     public float orbitSpeed = 10f;

//     private float currentAngle;

//     private void Start()
//     {
//         currentAngle = Mathf.Atan2(
//             transform.position.z - center.position.z,
//             transform.position.x - center.position.x
//         ) * Mathf.Rad2Deg;
//     }

//     private void Update()
//     {
//         TimeController timeController =
//             // FindFirstObjectByType<TimeController>();
//             FindAnyObjectByType<TimeController>();

//         if (timeController == null)
//             return;

//         currentAngle += orbitSpeed *
//                         Time.deltaTime *
//                         timeController.timeScale;

//         float angleRadians = currentAngle * Mathf.Deg2Rad;

//         float x = center.position.x +
//                   Mathf.Cos(angleRadians) * orbitRadius;

//         float z = center.position.z +
//                   Mathf.Sin(angleRadians) * orbitRadius;

//         transform.position = new Vector3(
//             x,
//             transform.position.y,
//             z
//         );
//     }
// }


//script updates 3.0
// using UnityEngine;

// public class OrbitController : MonoBehaviour
// {
//     [Header("Orbit")]
//     public Transform center;

//     [Header("Prototype Orbit")]
//     public float orbitRadius = 10f;
//     public float orbitSpeed = 10f;

//     private float currentAngle;

//     private TimeController timeController;

//     private void Start()
//     {
//         timeController = FindAnyObjectByType<TimeController>();

//         if (center == null)
//         {
//             Debug.LogError(
//                 $"{gameObject.name}: Orbit center is not assigned."
//             );

//             return;
//         }

//         currentAngle = Mathf.Atan2(
//             transform.position.z - center.position.z,
//             transform.position.x - center.position.x
//         ) * Mathf.Rad2Deg;
//     }

//     private void Update()
//     {
//         if (timeController == null || center == null)
//             return;

//         currentAngle +=
//             orbitSpeed *
//             Time.deltaTime *
//             (float)timeController.simulationDaysPerSecond;

//         float angleRadians =
//             currentAngle * Mathf.Deg2Rad;

//         float x =
//             center.position.x +
//             Mathf.Cos(angleRadians) * orbitRadius;

//         float z =
//             center.position.z +
//             Mathf.Sin(angleRadians) * orbitRadius;

//         transform.position = new Vector3(
//             x,
//             transform.position.y,
//             z
//         );
//     }
// }

//script updates 4.0

using UnityEngine;

public class OrbitController : MonoBehaviour
{
    [Header("Scientific Orbital Data")]
    public PlanetData data;
    public Transform center;

    private TimeController timeController;

    private void Start()
    {
        timeController = FindAnyObjectByType<TimeController>();

        if (data == null)
        {
            Debug.LogError(
                $"{gameObject.name}: PlanetData is not assigned.",
                this
            );
            enabled = false;
            return;
        }

        if (center == null)
        {
            Debug.LogError(
                $"{gameObject.name}: Orbit center is not assigned.",
                this
            );
            enabled = false;
            return;
        }

        if (timeController == null)
        {
            Debug.LogError(
                $"{gameObject.name}: TimeController was not found.",
                this
            );
            enabled = false;
        }
    }

    private void Update()
    {
        if (data == null || center == null || timeController == null)
            return;

        Vector3 orbitalPosition = KeplerSolver.CalculatePosition(
            data,
            timeController.JulianDate
        );

        transform.position = center.position + orbitalPosition;
    }
}
