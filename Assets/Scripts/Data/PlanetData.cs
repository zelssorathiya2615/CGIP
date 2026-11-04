// using UnityEngine;

// public class PlanetData : MonoBehaviour
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

[CreateAssetMenu(
    fileName = "PlanetData",
    menuName = "Solar System/Planet Data"
)]
public class PlanetData : ScriptableObject
{
    [Header("Identity")]
    public string planetName;

    [Header("JPL Keplerian Elements at J2000")]
    public double semiMajorAxisAU;
    public double eccentricity;
    public double inclinationDeg;
    public double meanLongitudeDeg;
    public double longitudeOfPerihelionDeg;
    public double longitudeOfAscendingNodeDeg;

    [Header("Rates per Julian Century")]
    public double semiMajorAxisRate;
    public double eccentricityRate;
    public double inclinationRate;
    public double meanLongitudeRate;
    public double longitudeOfPerihelionRate;
    public double longitudeOfAscendingNodeRate;

    [Header("JPL Additional Mean-Anomaly Terms")]
    public double b;
    public double c;
    public double s;
    public double f;

    [Header("Model Information")]
    // public int validFromYear = 1800;
    // public int validToYear = 2050;
    public int validFromYear = -3000;
    public int validToYear = 3000;

    public string source =
        "NASA/JPL Solar System Dynamics - Approximate Positions of the Planets";
}