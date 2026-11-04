// using UnityEngine;

// [CreateAssetMenu(fileName = "NewScriptableObjectScript", menuName = "Scriptable Objects/NewScriptableObjectScript")]
// public class NewScriptableObjectScript : ScriptableObject
// {
    
// }



using UnityEngine;

[CreateAssetMenu(
    fileName = "CelestialBodyInfo",
    menuName = "Solar System/Celestial Body Info"
)]
public class CelestialBodyInfo : ScriptableObject
{
    [Header("Identity")]
    public string bodyName;
    public string classification;
    public string positionFromSun;

    [Header("Overview")]
    [TextArea(4, 10)]
    public string overview;

    [TextArea(3, 8)]
    public string keyCharacteristics;

    [Header("Physical Properties")]
    public double meanRadiusKm;
    public double meanDiameterKm;
    public double massKg;
    public double meanDensityGcm3;
    public double surfaceGravityMs2;
    public double escapeVelocityKms;

    [Header("Orbital Properties")]
    public double semiMajorAxisAU;
    public double orbitalPeriodDays;
    public double orbitalEccentricity;
    public double orbitalInclinationDeg;
    public double perihelionAU;
    public double aphelionAU;
    public double meanOrbitalVelocityKms;

    [Header("Rotation Properties")]
    public double siderealRotationPeriodDays;
    public double lengthOfDayHours;
    public double axialTiltDeg;

    [Header("Atmosphere")]
    [TextArea(3, 8)]
    public string atmosphericComposition;

    public double atmosphericPressureBar;
    public double minimumTemperatureC;
    public double meanTemperatureC;
    public double maximumTemperatureC;

    [Header("Natural Satellites")]
    public int moonCount;

    public string[] notableMoons;

    [Header("Structure and Features")]
    [TextArea(3, 8)]
    public string internalStructure;

    [TextArea(3, 8)]
    public string surfaceAndRings;

    [TextArea(3, 8)]
    public string magneticField;
}
