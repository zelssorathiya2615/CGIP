// using UnityEngine;

// public class OrbitVisualizer : MonoBehaviour
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

// [ExecuteAlways]
// public class OrbitVisualizer : MonoBehaviour
// {
//     public float radius = 10f;
//     public int segments = 128;

//     private LineRenderer lineRenderer;

//     private void OnEnable()
//     {
//         SetupLineRenderer();
//         DrawOrbit();
//     }

//     private void OnValidate()
//     {
//         SetupLineRenderer();
//         DrawOrbit();
//     }

//     private void SetupLineRenderer()
//     {
//         if (lineRenderer == null)
//         {
//             lineRenderer = GetComponent<LineRenderer>();

//             if (lineRenderer == null)
//             {
//                 lineRenderer = gameObject.AddComponent<LineRenderer>();
//             }
//         }

//         lineRenderer.loop = true;
//         lineRenderer.useWorldSpace = false;
//         lineRenderer.positionCount = segments;
//         lineRenderer.widthMultiplier = 0.03f;
//     }

//     private void DrawOrbit()
//     {
//         if (lineRenderer == null || segments < 3)
//             return;

//         for (int i = 0; i < segments; i++)
//         {
//             float angle = i * Mathf.PI * 2f / segments;

//             float x = Mathf.Cos(angle) * radius;
//             float z = Mathf.Sin(angle) * radius;

//             lineRenderer.SetPosition(
//                 i,
//                 new Vector3(x, 0f, z)
//             );
//         }
//     }
// }





using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(LineRenderer))]
public class OrbitVisualizer : MonoBehaviour
{
    [Header("Scientific Orbit Data")]
    public PlanetData planetData;
    public TimeController timeController;

    [Header("Orbit Display")]
    [Range(32, 512)]
    public int segments = 256;

    public float lineWidth = 0.03f;

    private LineRenderer lineRenderer;

    private double lastJulianDate = double.NaN;

    private void OnEnable()
    {
        SetupLineRenderer();
        DrawOrbit();
    }

    private void OnValidate()
    {
        segments = Mathf.Max(32, segments);

        SetupLineRenderer();
        DrawOrbit();
    }

    private void Update()
    {
        if (Application.isPlaying)
        {
            if (timeController == null)
                timeController = FindAnyObjectByType<TimeController>();

            if (timeController != null &&
                timeController.JulianDate != lastJulianDate)
            {
                DrawOrbit();
            }
        }
    }

    private void SetupLineRenderer()
    {
        if (lineRenderer == null)
            lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.loop = true;
        lineRenderer.useWorldSpace = false;

        lineRenderer.positionCount = segments;

        lineRenderer.widthMultiplier = lineWidth;

        lineRenderer.shadowCastingMode =
            UnityEngine.Rendering.ShadowCastingMode.Off;

        lineRenderer.receiveShadows = false;

        lineRenderer.alignment = LineAlignment.View;
    }

    private void DrawOrbit()
    {
        if (planetData == null || lineRenderer == null)
            return;

        double julianDate = 2451545.0;

        if (Application.isPlaying && timeController != null)
            julianDate = timeController.JulianDate;

        lineRenderer.positionCount = segments;

        for (int i = 0; i < segments; i++)
        {
            double trueAnomaly =
                (2.0 * System.Math.PI * i) / segments;

            Vector3 point = KeplerSolver.CalculateOrbitPoint(
                planetData,
                julianDate,
                trueAnomaly
            );

            lineRenderer.SetPosition(i, point);
        }

        lastJulianDate = julianDate;
    }
}
