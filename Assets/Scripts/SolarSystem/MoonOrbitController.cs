// using UnityEngine;

// public class MoonOrbitController : MonoBehaviour
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





using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class MoonOrbitController : MonoBehaviour
{
    [Header("References")]
    public Transform earth;
    public Transform moon;
    public TimeController timeController;

    [Header("Moon Orbital Data")]
    public float orbitRadius = 3f;
    public float orbitalPeriodDays = 27.321661f;
    public float inclinationDegrees = 5.145f;

    [Header("Orbit Display")]
    [Range(64, 512)]
    public int segments = 256;

    [Header("Tidal Locking")]
    public bool enableTidalLock = true;
    public Vector3 tidalLockRotationOffset = Vector3.zero;

    public float lineWidth = 0.015f;

    private LineRenderer lineRenderer;

    private const double J2000 = 2451545.0;
    private double initialPhaseRadians = 0.0;

    private void Start()
    {
        SetupReferences();
        SetupLineRenderer();
        DrawOrbit();
    }

    private void OnValidate()
    {
        orbitRadius = Mathf.Max(0.01f, orbitRadius);
        orbitalPeriodDays = Mathf.Max(0.01f, orbitalPeriodDays);
        segments = Mathf.Max(64, segments);

        if (Application.isPlaying)
        {
            SetupLineRenderer();
            DrawOrbit();
        }
    }

    private void SetupReferences()
    {
        if (timeController == null)
            timeController = FindAnyObjectByType<TimeController>();

        if (lineRenderer == null)
            lineRenderer = GetComponent<LineRenderer>();
    }

    private void SetupLineRenderer()
    {
        if (lineRenderer == null)
            lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.useWorldSpace = false;
        lineRenderer.loop = true;
        lineRenderer.positionCount = segments;
        lineRenderer.widthMultiplier = lineWidth;
        lineRenderer.shadowCastingMode =
            UnityEngine.Rendering.ShadowCastingMode.Off;
        lineRenderer.receiveShadows = false;
    }

    private void Update()
    {
        if (earth == null || moon == null || timeController == null)
            return;

        // Keep the orbit centre attached to Earth's position.
        transform.position = earth.position;

        // Calculate elapsed simulation days since J2000.
        double elapsedDays =
            timeController.JulianDate - J2000;

        // Moon's orbital angular position.
        double angle =
            initialPhaseRadians +
            (2.0 * System.Math.PI * elapsedDays / orbitalPeriodDays);

        // Calculate position in the inclined orbital plane.
        float x = orbitRadius * (float)System.Math.Cos(angle);

        float z = orbitRadius * (float)System.Math.Sin(angle);

        float inclination = inclinationDegrees * Mathf.Deg2Rad;

        float y = z * Mathf.Sin(inclination);
        z = z * Mathf.Cos(inclination);

        moon.localPosition = new Vector3(x, y, z);
        

 

        // Keep the same lunar face pointing toward Earth.
        if (enableTidalLock)
        {
            Vector3 directionToEarth =
                earth.position - moon.position;

            // Normal direction of the inclined orbital plane.
            Vector3 orbitNormal = transform.TransformDirection(
                new Vector3(
                    0f,
                    Mathf.Cos(inclination),
                    Mathf.Sin(inclination)
                )
            ).normalized;

            if (directionToEarth.sqrMagnitude > 0.000001f)
            {
                moon.rotation =
                    Quaternion.LookRotation(directionToEarth, orbitNormal)
                    * Quaternion.Euler(tidalLockRotationOffset);
            }
        }
    }

    private void DrawOrbit()
    {
        if (lineRenderer == null)
            return;

        lineRenderer.positionCount = segments;

        float inclination = inclinationDegrees * Mathf.Deg2Rad;

        for (int i = 0; i < segments; i++)
        {
            float angle =
                2f * Mathf.PI * i / segments;

            float x = orbitRadius * Mathf.Cos(angle);
            float z = orbitRadius * Mathf.Sin(angle);

            float y = z * Mathf.Sin(inclination);
            z = z * Mathf.Cos(inclination);

            lineRenderer.SetPosition(
                i,
                new Vector3(x, y, z)
            );
        }
    }
}
