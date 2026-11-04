// using UnityEngine;

// public class PlanetSelectionManager : MonoBehaviour
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

// public class PlanetSelectionManager : MonoBehaviour
// {
//     [Header("Selected Body")]
//     public Transform selectedBody;

//     [Header("Information Panel")]
//     public TMP_Text selectedBodyName;
//     public TMP_Text bodyCategory;
//     public TMP_Text bodyDescription;

//     public void SelectBody(
//         Transform body,
//         string displayName,
//         string category,
//         string description)
//     {
//         if (body == null)
//         {
//             Debug.LogWarning("Selected body is missing.");
//             return;
//         }

//         selectedBody = body;

//         if (selectedBodyName != null)
//             selectedBodyName.text = displayName.ToUpper();

//         if (bodyCategory != null)
//             bodyCategory.text = category.ToUpper();

//         if (bodyDescription != null)
//             bodyDescription.text = description;

//         Debug.Log("Selected celestial body: " + displayName);
//     }
// }







// using TMPro;
// using UnityEngine;

// public class PlanetSelectionManager : MonoBehaviour
// {
//     [Header("Selected Body")]
//     public Transform selectedBody;

//     [Header("Information Panel")]
//     public TMP_Text selectedBodyName;
//     public TMP_Text bodyCategory;
//     public TMP_Text bodyDescription;

//     public void SelectBody(
//         Transform body,
//         CelestialBodyInfo info)
//     {
//         if (body == null || info == null)
//         {
//             Debug.LogWarning("Missing body or information asset.");
//             return;
//         }

//         selectedBody = body;

//         if (selectedBodyName != null)
//             selectedBodyName.text = info.bodyName.ToUpper();

//         if (bodyCategory != null)
//             bodyCategory.text = info.classification.ToUpper();

//         if (bodyDescription != null)
//         {
//             bodyDescription.text =
//                 info.overview + "\n\n" +
//                 info.keyCharacteristics;
//         }

//         Debug.Log("Selected celestial body: " + info.bodyName);
//     }
// }





using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlanetSelectionManager : MonoBehaviour
{
    [Header("Selected Body")]
    public Transform selectedBody;

    [Header("Information Panel")]
    public TMP_Text selectedBodyName;
    public TMP_Text bodyCategory;
    public TMP_Text bodyDescription;

    [Header("Scroll View References")]
    public RectTransform contentRect;
    public ScrollRect informationScrollView;

    private CelestialBodyInfo currentInfo;

    private enum InformationTab
    {
        Overview,
        Physical,
        Orbit,
        More
    }

    public void SelectBody(Transform body, CelestialBodyInfo info)
    {
        if (body == null || info == null)
        {
            Debug.LogWarning("Missing body or information asset.");
            return;
        }

        selectedBody = body;
        currentInfo = info;

        if (selectedBodyName != null)
            selectedBodyName.text = info.bodyName.ToUpper();

        if (bodyCategory != null)
            bodyCategory.text = info.classification.ToUpper();

        ShowTab(InformationTab.Overview);

        Debug.Log("Selected celestial body: " + info.bodyName);
    }

    // Connect these four methods to the UI buttons.

    public void ShowOverview()
    {
        ShowTab(InformationTab.Overview);
    }

    public void ShowPhysical()
    {
        ShowTab(InformationTab.Physical);
    }

    public void ShowOrbit()
    {
        ShowTab(InformationTab.Orbit);
    }

    public void ShowMore()
    {
        ShowTab(InformationTab.More);
    }

    private void ShowTab(InformationTab tab)
    {
        if (currentInfo == null || bodyDescription == null)
            return;

        string content = "";

        switch (tab)
        {
            case InformationTab.Overview:
                content = BuildOverview();
                break;

            case InformationTab.Physical:
                content = BuildPhysical();
                break;

            case InformationTab.Orbit:
                content = BuildOrbit();
                break;

            case InformationTab.More:
                content = BuildMore();
                break;
        }

        bodyDescription.text = content;

        UpdateScrollContent();
    }

    private string BuildOverview()
    {
        StringBuilder sb = new StringBuilder();

        AddHeading(sb, "OVERVIEW");
        AddParagraph(sb, currentInfo.overview);

        AddHeading(sb, "KEY CHARACTERISTICS");
        AddParagraph(sb, currentInfo.keyCharacteristics);

        return sb.ToString();
    }

    private string BuildPhysical()
    {
        StringBuilder sb = new StringBuilder();

        AddHeading(sb, "PHYSICAL PROPERTIES");

        AddValue(sb, "Mean radius", currentInfo.meanRadiusKm, "km");
        AddValue(sb, "Mean diameter", currentInfo.meanDiameterKm, "km");
        AddScientificValue(sb, "Mass", currentInfo.massKg, "kg");
        AddValue(sb, "Mean density", currentInfo.meanDensityGcm3, "g/cm³");
        AddValue(sb, "Surface gravity", currentInfo.surfaceGravityMs2, "m/s²");
        AddValue(sb, "Escape velocity", currentInfo.escapeVelocityKms, "km/s");

        return sb.ToString();
    }

    private string BuildOrbit()
    {
        StringBuilder sb = new StringBuilder();

        AddHeading(sb, "ORBITAL PROPERTIES");

        AddValue(sb, "Semi-major axis", currentInfo.semiMajorAxisAU, "AU");
        AddValue(sb, "Orbital period", currentInfo.orbitalPeriodDays, "days");
        AddValue(sb, "Eccentricity", currentInfo.orbitalEccentricity, "");
        AddValue(sb, "Inclination", currentInfo.orbitalInclinationDeg, "°");
        AddValue(sb, "Perihelion", currentInfo.perihelionAU, "AU");
        AddValue(sb, "Aphelion", currentInfo.aphelionAU, "AU");
        AddValue(sb, "Mean orbital velocity", currentInfo.meanOrbitalVelocityKms, "km/s");

        return sb.ToString();
    }

    private string BuildMore()
    {
        StringBuilder sb = new StringBuilder();

        AddHeading(sb, "ROTATION");

        AddValue(sb, "Sidereal rotation period",
            currentInfo.siderealRotationPeriodDays, "days");

        AddValue(sb, "Length of day",
            currentInfo.lengthOfDayHours, "hours");

        AddValue(sb, "Axial tilt", currentInfo.axialTiltDeg, "°");

        AddHeading(sb, "ATMOSPHERE");
        AddParagraph(sb, currentInfo.atmosphericComposition);

        AddValue(sb, "Atmospheric pressure",
            currentInfo.atmosphericPressureBar, "bar");

        AddValue(sb, "Minimum temperature",
            currentInfo.minimumTemperatureC, "°C");

        AddValue(sb, "Mean temperature",
            currentInfo.meanTemperatureC, "°C");

        AddValue(sb, "Maximum temperature",
            currentInfo.maximumTemperatureC, "°C");

        AddHeading(sb, "NATURAL SATELLITES");

        AddValue(sb, "Moon count", currentInfo.moonCount, "");

        if (currentInfo.notableMoons != null &&
            currentInfo.notableMoons.Length > 0)
        {
            AddParagraph(sb,
                "Notable moons: " +
                string.Join(", ", currentInfo.notableMoons));
        }

        AddHeading(sb, "STRUCTURE AND FEATURES");

        AddSubheading(sb, "Internal Structure");
        AddParagraph(sb, currentInfo.internalStructure);

        AddSubheading(sb, "Surface and Rings");
        AddParagraph(sb, currentInfo.surfaceAndRings);

        AddSubheading(sb, "Magnetic Field");
        AddParagraph(sb, currentInfo.magneticField);

        return sb.ToString();
    }

    private void AddHeading(StringBuilder sb, string heading)
    {
        sb.AppendLine();
        sb.AppendLine("<b>" + heading + "</b>");
        sb.AppendLine();
    }

    private void AddSubheading(StringBuilder sb, string heading)
    {
        sb.AppendLine();
        sb.AppendLine("<b>" + heading + "</b>");
    }

    private void AddParagraph(StringBuilder sb, string paragraph)
    {
        if (!string.IsNullOrWhiteSpace(paragraph))
        {
            sb.AppendLine(paragraph);
            sb.AppendLine();
        }
    }

    private void AddValue(
        StringBuilder sb,
        string label,
        double value,
        string unit)
    {
        // Zero is treated as an unfilled value.
        if (value == 0 || double.IsNaN(value))
            return;

        sb.AppendLine(
            label + ": " + value.ToString("N4") +
            (string.IsNullOrEmpty(unit) ? "" : " " + unit)
        );
    }

    private void AddValue(
        StringBuilder sb,
        string label,
        int value,
        string unit)
    {
        if (value <= 0)
            return;

        sb.AppendLine(
            label + ": " + value +
            (string.IsNullOrEmpty(unit) ? "" : " " + unit)
        );
    }

    private void AddScientificValue(
        StringBuilder sb,
        string label,
        double value,
        string unit)
    {
        if (value <= 0 || double.IsNaN(value))
            return;

        sb.AppendLine(
            label + ": " + value.ToString("0.0000E+0") +
            " " + unit
        );
    }

    private void UpdateScrollContent()
    {
        if (contentRect == null || informationScrollView == null)
            return;

        Canvas.ForceUpdateCanvases();

        float requiredHeight =
            bodyDescription.preferredHeight + 25f;

        float viewportHeight =
            informationScrollView.viewport.rect.height;

        contentRect.sizeDelta = new Vector2(
            contentRect.sizeDelta.x,
            Mathf.Max(requiredHeight, viewportHeight)
        );

        Canvas.ForceUpdateCanvases();

        informationScrollView.verticalNormalizedPosition = 1f;
    }
}
